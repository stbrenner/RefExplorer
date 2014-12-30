//-----------------------------------------------------------------------------
// ExplorerConfiguration.cs
//
// Author: Stephan Brenner
// Date:   06/09/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace RefExplorer.Core.Configuration
{
    public class ExplorerConfiguration
    {
        private readonly List<AssemblyPath> assemblyPaths = new List<AssemblyPath>();
        private readonly List<string> targetExcludes = new List<string>();
        private readonly List<string> sourceExcludes = new List<string>();
        private const string outputFileName = "RefExp-Result.html";

        public string OutputFileName { get { return outputFileName; } }

        [XmlIgnore]
        public DirectoryInfo BaseDirectory { get; set; }
        public string BaseDirectoryString { get { return BaseDirectory.FullName; } set { BaseDirectory = new DirectoryInfo(value); } }

        public List<AssemblyPath> AssemblyPaths { get { return assemblyPaths; } }
        public List<string> TargetExcludes { get { return targetExcludes; } }
        public List<string> SourceExcludes { get { return sourceExcludes; } }

        public bool ShowNativeReferences { get; set; }

        public void SetAssemblyPaths(params string[] paths)
        {
            assemblyPaths.Clear();
            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                {
                    assemblyPaths.Add(new AssemblyDirectoryPath(path));
                }
                else if (File.Exists(path))
                {
                    assemblyPaths.Add(new AssemblyDirectPath(path));
                }
                else
                {
                    assemblyPaths.Add(new AssemblySearchPath(Path.GetDirectoryName(path), Path.GetFileName(path), SearchOption.AllDirectories));
                }
            }
        }
    }
}