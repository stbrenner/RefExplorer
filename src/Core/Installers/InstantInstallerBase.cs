using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace RefExplorer.Core.Installers
{
    public abstract class InstantInstallerBase : IDisposable
    {
        private readonly Dictionary<object, object> savedState = new Dictionary<object, object>();
        private readonly DirectoryInfo directory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));

        public DirectoryInfo Directory { get { return directory; } }


        public void Execute()
        {
            try
            {
                directory.Create();
                Install(savedState);
            }
            catch
            {
                Rollback(savedState);
                directory.Delete();
            }
            finally
            {
                Commit(savedState);
            }
        }

        public void Dispose()
        {
            Uninstall(savedState);
            directory.Delete(true);
        }

        protected abstract void Install(IDictionary state);

        protected abstract void Commit(IDictionary state);

        protected abstract void Rollback(IDictionary state);

        protected abstract void Uninstall(IDictionary state);

        protected void CreateFile(string path, byte[] data)
        {
            using (var fs = new FileStream(GetAbsolutePath(path), FileMode.Create, FileAccess.Write))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        private string GetAbsolutePath(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(directory.FullName, path);
            }
            return path;
        }

        protected void CreateFile(string path, Bitmap data)
        {
            data.Save(GetAbsolutePath(path));
        }

        protected void CreateFile(string path, string data)
        {
            using (var sw = new StreamWriter(GetAbsolutePath(path), false, Encoding.UTF8))
            {
                sw.Write(data);
            }
        }
    }
}
