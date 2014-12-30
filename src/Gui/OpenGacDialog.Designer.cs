namespace RefExplorer.Gui
{
  partial class OpenGacDialog
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.okButton = new System.Windows.Forms.Button();
        this.cancelButton = new System.Windows.Forms.Button();
        this.assemblyListView = new System.Windows.Forms.ListView();
        this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
        this.version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
        this.language = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
        this.publicKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
        this.platform = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
        this.frameworkVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
        this.SuspendLayout();
        // 
        // okButton
        // 
        this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.okButton.Location = new System.Drawing.Point(573, 431);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(75, 23);
        this.okButton.TabIndex = 1;
        this.okButton.Text = "&OK";
        this.okButton.UseVisualStyleBackColor = true;
        // 
        // cancelButton
        // 
        this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.cancelButton.Location = new System.Drawing.Point(654, 431);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(75, 23);
        this.cancelButton.TabIndex = 2;
        this.cancelButton.Text = "&Cancel";
        this.cancelButton.UseVisualStyleBackColor = true;
        // 
        // assemblyListView
        // 
        this.assemblyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.assemblyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.version,
            this.language,
            this.publicKey,
            this.platform,
            this.frameworkVersion});
        this.assemblyListView.FullRowSelect = true;
        this.assemblyListView.HideSelection = false;
        this.assemblyListView.Location = new System.Drawing.Point(12, 12);
        this.assemblyListView.Name = "assemblyListView";
        this.assemblyListView.Size = new System.Drawing.Size(717, 413);
        this.assemblyListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
        this.assemblyListView.TabIndex = 0;
        this.assemblyListView.UseCompatibleStateImageBehavior = false;
        this.assemblyListView.View = System.Windows.Forms.View.Details;
        this.assemblyListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.AssemblyListViewColumnClick);
        this.assemblyListView.DoubleClick += new System.EventHandler(this.AssemblyListViewDoubleClick);
        // 
        // name
        // 
        this.name.Text = "Name";
        this.name.Width = 250;
        // 
        // version
        // 
        this.version.Text = "Version";
        this.version.Width = 120;
        // 
        // language
        // 
        this.language.Text = "Language";
        // 
        // publicKey
        // 
        this.publicKey.Text = "Public Key";
        this.publicKey.Width = 127;
        // 
        // platform
        // 
        this.platform.Text = "Platform";
        // 
        // frameworkVersion
        // 
        this.frameworkVersion.Text = "Framework";
        this.frameworkVersion.Width = 70;
        // 
        // OpenGacDialog
        // 
        this.AcceptButton = this.okButton;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.cancelButton;
        this.ClientSize = new System.Drawing.Size(741, 466);
        this.Controls.Add(this.assemblyListView);
        this.Controls.Add(this.cancelButton);
        this.Controls.Add(this.okButton);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.MinimumSize = new System.Drawing.Size(100, 100);
        this.Name = "OpenGacDialog";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Explore Global Assembly";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.ListView assemblyListView;
    private System.Windows.Forms.ColumnHeader name;
    private System.Windows.Forms.ColumnHeader version;
    private System.Windows.Forms.ColumnHeader language;
    private System.Windows.Forms.ColumnHeader publicKey;
    private System.Windows.Forms.ColumnHeader platform;
    private System.Windows.Forms.ColumnHeader frameworkVersion;

  }
}