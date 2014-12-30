using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using RefExplorer.Core.Assemblies;

namespace RefExplorer.Gui
{
    public partial class OpenGacDialog : Form
    {
        private readonly List<AssemblyFileInfo> selectedAssemblies = new List<AssemblyFileInfo>();

        public OpenGacDialog()
        {
            InitializeComponent();
        }

        public List<AssemblyFileInfo> SelectedAssemblies { get { return selectedAssemblies; } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var gacInfo = new GacInfo();
            AssemblyFileInfo[] assemblies = gacInfo.GetAssemblyFiles(null, null, null, null);

            var list = new List<ListViewItem>();
            foreach (AssemblyFileInfo assembly in assemblies)
            {
                var item = new ListViewItem(new[]
                                                {
                                                    assembly.AssemblyName,
                                                    assembly.Version == null ? string.Empty : assembly.Version.ToString(),
                                                    assembly.CultureInfo.ToString(),
                                                    assembly.PublicKeyToken.ToString(),
                                                    assembly.TargetPlatform.ToString(),
                                                    assembly.FrameworkVersion == null ? string.Empty : assembly.FrameworkVersion.ToString()
                                                }) {Tag = assembly};
                list.Add(item);
            }
            assemblyListView.Items.AddRange(list.ToArray());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
            {
                foreach (ListViewItem selectedItem in assemblyListView.SelectedItems)
                {
                    var fileInfo = (AssemblyFileInfo)selectedItem.Tag;
                    VerifyPlatform(fileInfo);
                    selectedAssemblies.Add(fileInfo);
                }
            }
        }

        private static void VerifyPlatform(AssemblyFileInfo fileInfo)
        {
            if (fileInfo.TargetPlatform == TargetPlatform.X32 && IntPtr.Size == 8)
            {
                throw new PlatformNotSupportedException("You can't load a 32 bit assembly into a 64 bit process.");
            }

            if (fileInfo.TargetPlatform == TargetPlatform.X64 && IntPtr.Size == 4)
            {
                throw new PlatformNotSupportedException("You can't load a 64 bit assembly into a 32 bit process.");
            }
        }

        private void AssemblyListViewDoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AssemblyListViewColumnClick(object sender, ColumnClickEventArgs e)
        {
            assemblyListView.ListViewItemSorter = new ListViewItemComparer(e.Column);
            assemblyListView.Sort();
        }

        private class ListViewItemComparer : IComparer
        {
            private readonly int column;

            public ListViewItemComparer(int column)
            {
                this.column = column;
            }

            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[column].Text, ((ListViewItem)y).SubItems[column].Text);
            }
        }

    }
}