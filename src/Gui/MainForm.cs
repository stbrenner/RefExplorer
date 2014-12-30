using RefExplorer.Core;
using RefExplorer.Core.Collections;
using RefExplorer.Core.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RefExplorer.Gui
{
    public partial class MainForm : Form
    {
        private readonly ConfigFile<Configuration> configFile = new ConfigFile<Configuration>();
        private readonly ExplorerEngine engine = new ExplorerEngine();
        private readonly DirectoryInfo baseDirectory;
        private bool optionIsDirty;

        public MainForm(DirectoryInfo baseDirectory)
        {
            this.baseDirectory = baseDirectory;
            InitializeComponent();
            engine.Configuration.BaseDirectory = baseDirectory;
            ShowWelcome();
        }

        protected override void OnLoad(EventArgs e)
        {
            var config = configFile.Exists() ? configFile.Load() : Configuration.GetDefault();

            zoomComboBox.SelectedIndex = 2;
            excludeBox.Text = config.HideReferencesTo;
            doNotExploreBox.Text = config.HideReferencesFrom;
            ShowPInvoke.Checked = config.ShowNativeReferences;
            if (config.EntryPoints != null)
            {
                foreach (var entryPoint in config.EntryPoints)
                {
                    inputFilesControl.TryAddItem(entryPoint);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var configuration = new Configuration
                                              {
                                                  HideReferencesTo = excludeBox.Text,
                                                  HideReferencesFrom = doNotExploreBox.Text,
                                                  ShowNativeReferences = ShowPInvoke.Checked,
                                                  EntryPoints = inputFilesControl.GetFiles()
                                              };
            configFile.Save(configuration);
        }

        private void ShowWelcome()
        {
            webBrowser.Navigate(Path.Combine(baseDirectory.FullName, "Welcome.html"));
        }

        private void Explore()
        {
            webBrowser.Navigate(Path.Combine(baseDirectory.FullName, "InProgress.html"));
            engine.Configuration.SetAssemblyPaths(inputFilesControl.GetFiles());
            engine.Configuration.ShowNativeReferences = ShowPInvoke.Checked;
            CollectionUtil.Replace(excludeBox.Text.Split(';'), engine.Configuration.TargetExcludes);
            CollectionUtil.Replace(doNotExploreBox.Text.Split(';'), engine.Configuration.SourceExcludes);
            ThreadPool.QueueUserWorkItem(ExploreThread, this);
        }

        private static void ExploreThread(object state)
        {
            var mainForm = (MainForm)state;
            ExplorerConfiguration config = mainForm.engine.Configuration;

            try
            {
                mainForm.engine.Explore();
                mainForm.webBrowser.Navigate(Path.Combine(config.BaseDirectory.FullName, config.OutputFileName));
            }
            catch (Exception e)
            {
                mainForm.webBrowser.Navigate(Path.Combine(config.BaseDirectory.FullName, "Welcome.html"));
                MessageBox.Show(e.Message, Resources.ApplicationTitle);
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            inputFilesControl.TryAddItem(GetFileFromDataObject(e.Data));
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string file = GetFileFromDataObject(e.Data);
            if (IsInputFile(file))
            {
                e.Effect = DragDropEffects.Link;
            }
        }

        private static bool IsInputFile(string file)
        {
            string ext = Path.GetExtension(file);
            return string.Compare(ext, ".DLL", true) == 0 || string.Compare(ext, ".EXE", true) == 0 ||
                   Directory.Exists(file);
        }

        private static string GetFileFromDataObject(IDataObject dataObject)
        {
            string[] data = (string[])dataObject.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                return data[0];
            }
            return null;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            webBrowser.ShowPrintPreviewDialog();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            webBrowser.ShowSaveAsDialog();
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.OriginalString == "about:homepage")
            {
                Process.Start("http://www.stephan-brenner.com");
                e.Cancel = true;
            }
            else if (IsInputFile(e.Url.AbsolutePath))
            {
                inputFilesControl.TryAddItem(e.Url.AbsolutePath);
                Explore();
                e.Cancel = true;
            }
            else if (!e.Url.OriginalString.StartsWith(baseDirectory.FullName))
            {
                MessageBox.Show(Resources.WrongDrop, Resources.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForm form = new InfoForm();
            form.ShowDialog();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Explore();
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                engine.Configuration = ConfigurationFile.Open(openFileDialog.FileName);
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ConfigurationFile.Save(saveFileDialog.FileName, engine.Configuration);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 2 &&
                 (inputFilesControl.IsDirty || optionIsDirty))
            {
                Explore();
                inputFilesControl.IsDirty = false;
                optionIsDirty = false;
            }
        }

        private void OptionTextChanged(object sender, EventArgs e)
        {
            optionIsDirty = true;
        }

        private void zoomComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int factor = Convert.ToInt32(zoomComboBox.Text.Replace("%", ""));
                webBrowser.Zoom(factor);
            }
            catch
            {
                MessageBox.Show("For the zoom functionality you need at least Internet Explorer 7 installed.");
            }
        }

        private void ShowPInvoke_CheckedChanged(object sender, EventArgs e)
        {
            optionIsDirty = true;
        }

        private void defaultsButton_Click(object sender, EventArgs e)
        {
            var config = Configuration.GetDefault();
            excludeBox.Text = config.HideReferencesTo;
            doNotExploreBox.Text = config.HideReferencesFrom;
            ShowPInvoke.Checked = config.ShowNativeReferences;
        }
    }
}