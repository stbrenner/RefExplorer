namespace RefExplorer.Gui
{
  partial class InputFilesControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.entryAssembliesBox = new System.Windows.Forms.ListBox();
      this.addAssembly = new System.Windows.Forms.Button();
      this.addFolder = new System.Windows.Forms.Button();
      this.addGlobalAssembly = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.deleteButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // entryAssembliesBox
      // 
      this.entryAssembliesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.entryAssembliesBox.FormattingEnabled = true;
      this.entryAssembliesBox.HorizontalScrollbar = true;
      this.entryAssembliesBox.IntegralHeight = false;
      this.entryAssembliesBox.Location = new System.Drawing.Point(11, 29);
      this.entryAssembliesBox.Name = "entryAssembliesBox";
      this.entryAssembliesBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.entryAssembliesBox.Size = new System.Drawing.Size(451, 134);
      this.entryAssembliesBox.Sorted = true;
      this.entryAssembliesBox.TabIndex = 0;
      this.entryAssembliesBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.entryAssembliesBox_KeyDown);
      // 
      // addAssembly
      // 
      this.addAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.addAssembly.Location = new System.Drawing.Point(468, 26);
      this.addAssembly.Name = "addAssembly";
      this.addAssembly.Size = new System.Drawing.Size(136, 23);
      this.addAssembly.TabIndex = 1;
      this.addAssembly.Text = "Add &EXE/DLL...";
      this.addAssembly.UseVisualStyleBackColor = true;
      this.addAssembly.Click += new System.EventHandler(this.addAssembly_Click);
      // 
      // addFolder
      // 
      this.addFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.addFolder.Location = new System.Drawing.Point(468, 55);
      this.addFolder.Name = "addFolder";
      this.addFolder.Size = new System.Drawing.Size(136, 23);
      this.addFolder.TabIndex = 2;
      this.addFolder.Text = "Add &Folder...";
      this.addFolder.UseVisualStyleBackColor = true;
      this.addFolder.Click += new System.EventHandler(this.addFolder_Click);
      // 
      // addGlobalAssembly
      // 
      this.addGlobalAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.addGlobalAssembly.Location = new System.Drawing.Point(468, 84);
      this.addGlobalAssembly.Name = "addGlobalAssembly";
      this.addGlobalAssembly.Size = new System.Drawing.Size(136, 23);
      this.addGlobalAssembly.TabIndex = 3;
      this.addGlobalAssembly.Text = "Add &Global Assembly...";
      this.addGlobalAssembly.UseVisualStyleBackColor = true;
      this.addGlobalAssembly.Click += new System.EventHandler(this.addGlobalAssembly_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(8, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(268, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Entry points from where the dependency analysis starts:";
      // 
      // openFileDialog
      // 
      this.openFileDialog.Filter = "Assemblies|*.exe;*.dll";
      this.openFileDialog.Multiselect = true;
      this.openFileDialog.Title = "Select Assembly";
      // 
      // folderBrowserDialog
      // 
      this.folderBrowserDialog.Description = "Select the folder where the assemblies are located.";
      this.folderBrowserDialog.ShowNewFolderButton = false;
      // 
      // deleteButton
      // 
      this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.deleteButton.Location = new System.Drawing.Point(468, 140);
      this.deleteButton.Name = "deleteButton";
      this.deleteButton.Size = new System.Drawing.Size(136, 23);
      this.deleteButton.TabIndex = 5;
      this.deleteButton.Text = "&Delete";
      this.deleteButton.UseVisualStyleBackColor = true;
      this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
      // 
      // InputFilesControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.deleteButton);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.addGlobalAssembly);
      this.Controls.Add(this.addFolder);
      this.Controls.Add(this.addAssembly);
      this.Controls.Add(this.entryAssembliesBox);
      this.Name = "InputFilesControl";
      this.Size = new System.Drawing.Size(615, 177);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox entryAssembliesBox;
    private System.Windows.Forms.Button addAssembly;
    private System.Windows.Forms.Button addFolder;
    private System.Windows.Forms.Button addGlobalAssembly;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.Button deleteButton;
  }
}
