using System;
using System.Windows.Forms;
using RefExplorer.Core.Collections;

namespace RefExplorer.Gui
{
  public partial class InputFilesControl : UserControl
  {
    public InputFilesControl()
    {
      InitializeComponent();
    }

    public bool IsDirty { get; set; }

    public string[] GetFiles()
    {
      return CollectionUtil.ToArray<string>(entryAssembliesBox.Items);
    }

    private void addAssembly_Click(object sender, EventArgs e)
    {
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        foreach (string fileName in openFileDialog.FileNames)
        {
          TryAddItem(fileName);
        }
      }
    }

    private void addFolder_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        TryAddItem(folderBrowserDialog.SelectedPath);
      }
    }

    public void TryAddItem(string item)
    {
      if (!entryAssembliesBox.Items.Contains(item))
      {
        entryAssembliesBox.Items.Add(item);
        IsDirty = true;
      }
    }

    private void addGlobalAssembly_Click(object sender, EventArgs e)
    {
      var openGacDialog = new OpenGacDialog();
      if (openGacDialog.ShowDialog() == DialogResult.OK)
      {
        foreach (var selectedAssembly in openGacDialog.SelectedAssemblies)
        {
          TryAddItem(selectedAssembly.FullName);
        }
      }
    }

    private void entryAssembliesBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 46)   // Backspace key
      {
        DeleteItem();
      }
    }

    private void DeleteItem()
    {
      var selectedIndex = entryAssembliesBox.SelectedIndex;

      var toDelete = new object[entryAssembliesBox.SelectedItems.Count];
      entryAssembliesBox.SelectedItems.CopyTo(toDelete, 0);

      foreach (var item in toDelete)
      {
        entryAssembliesBox.Items.Remove(item);
        IsDirty = true;
      }

      // Select next item
      for (var index = selectedIndex; index >= 0; index--)
      {
        if (index < entryAssembliesBox.Items.Count)
        {
          entryAssembliesBox.SetSelected(index, true);
          break;
        }
      }
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      DeleteItem();
    }
  }
}
