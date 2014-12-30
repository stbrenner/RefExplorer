//-----------------------------------------------------------------------------
// File:    Program.cs
// Author:  snb
// Created: 12/26/2006
//-----------------------------------------------------------------------------

using System;
using System.Threading;
using System.Windows.Forms;

namespace RefExplorer.Gui
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      using (var instantInstaller = new InstantInstaller())
      {
        instantInstaller.Execute();
        Application.ThreadException += ApplicationThreadException;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm(instantInstaller.Directory));
      }
    }

    public static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
    {
      MessageBox.Show(e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
}