using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace RefExplorer.Gui
{
  public partial class InfoForm : Form
  {
    public InfoForm()
    {
      InitializeComponent();
    }

    private void InfoForm_Load(object sender, EventArgs e)
    {
      Assembly assembly = Assembly.GetExecutingAssembly();
      AssemblyName name = assembly.GetName();
      infoBox.Text = string.Format(infoBox.Text, name.Version);
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://www.stephan-brenner.com/");
    }
  }
}