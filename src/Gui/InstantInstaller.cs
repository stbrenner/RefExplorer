using RefExplorer.Core.Installers;
using System.Collections;

namespace RefExplorer.Gui
{
    internal class InstantInstaller : InstantInstallerBase
    {
        protected override void Install(IDictionary state)
        {
            CreateFile("ActivityIndicator.gif", Resources.ActivityIndicator);
            CreateFile("Default.css", Resources.DefaultCss);
            CreateFile("InProgress.html", Resources.InProgressHtml);
            CreateFile("Welcome.html", Resources.WelcomeHtml);

            CreateFile("dot.exe", GraphvizResources.dot);
            CreateFile("freetype6.dll", GraphvizResources.freetype6);
            CreateFile("jpeg.dll", GraphvizResources.jpeg);
            CreateFile("libexpat.dll", GraphvizResources.libexpat);
            CreateFile("libexpatw.dll", GraphvizResources.libexpatw);
            CreateFile("png.dll", GraphvizResources.png);
            CreateFile("z.dll", GraphvizResources.z);
            CreateFile("zlib1.dll", GraphvizResources.zlib1);
       }

        protected override void Commit(IDictionary state)
        {
        }

        protected override void Rollback(IDictionary state)
        {
        }

        protected override void Uninstall(IDictionary state)
        {
        }
    }
}