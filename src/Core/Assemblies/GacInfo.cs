using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace RefExplorer.Core.Assemblies
{
    public class GacInfo
    {
        private readonly DirectoryInfo[] gacDirs = 
        {
            new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "assembly")),
            new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), @"Microsoft.NET\assembly"))
        };

        /// <returns>Null, if assembly not found.</returns>
        public DirectoryInfo GetAssemblyDirectory(AssemblyName assemblyName)
        {
            DirectoryInfo[] assemblyDirectories = GetAssemblyDirectories(assemblyName.Name, assemblyName.Version, assemblyName.CultureInfo, new PublicKeyToken(assemblyName));
            Debug.Assert(assemblyDirectories.Length == 0 || assemblyDirectories.Length == 1);
            return assemblyDirectories.Length > 0 ? assemblyDirectories[0] : null;
        }

        /// <param name="name">Name of assembly or null if you do not want to filter after it.</param>
        /// <param name="version">Version of assembly or null if you do not want to filter after it.</param>
        /// <param name="cultureInfo">Culture of assembly or null if you do not want to filter after it.</param>
        /// <param name="publicKeyToken">Public key token of assembly or null if you do not want to filter after it.</param>
        public AssemblyFileInfo[] GetAssemblyFiles(string name, Version version, CultureInfo cultureInfo, PublicKeyToken publicKeyToken)
        {
            DirectoryInfo[] assemblyNameDirectories = GetAssemblyNameDirectories(name);

            List<AssemblyFileInfo> result = new List<AssemblyFileInfo>();
            foreach (DirectoryInfo assemblyNameDirectory in assemblyNameDirectories)
            {
                DirectoryInfo[] assemblyDirectories = GetAssemblyDirectories(assemblyNameDirectory, version, cultureInfo, publicKeyToken);
                foreach (DirectoryInfo assemblyDirectory in assemblyDirectories)
                {
                    FileInfo[] files = assemblyDirectory.GetFiles("*.*");
                    if (!TryGetAssembly(files, assemblyNameDirectory.Name, ".dll", result))
                    {
                        if (!TryGetAssembly(files, assemblyNameDirectory.Name, ".exe", result))
                        {
                            if (!TryGetAssembly(files, null, ".dll", result))
                            {
                                TryGetAssembly(files, null, ".exe", result);
                            }
                        }
                    }
                }
            }
            return result.ToArray();
        }

        private bool TryGetAssembly(IEnumerable<FileInfo> files, string name, string extension, ICollection<AssemblyFileInfo> result)
        {
            foreach (FileInfo file in files)
            {
                if ((string.IsNullOrEmpty(name) || string.Compare(name, Path.GetFileNameWithoutExtension(file.FullName), true) == 0) &&
                    string.Compare(extension, Path.GetExtension(file.FullName), true) == 0)
                {
                    result.Add(AssemblyFileInfo.FromGacFile(file));
                    return true;
                }
            }
            return false;
        }

        /// <param name="name">Name of assembly or null if you do not want to filter after it.</param>
        /// <param name="version">Version of assembly or null if you do not want to filter after it.</param>
        /// <param name="cultureInfo">Culture of assembly or null if you do not want to filter after it.</param>
        /// <param name="publicKeyToken">Public key token of assembly or null if you do not want to filter after it.</param>
        public DirectoryInfo[] GetAssemblyDirectories(string name, Version version, CultureInfo cultureInfo, PublicKeyToken publicKeyToken)
        {
            DirectoryInfo[] assemblyNameDirectories = GetAssemblyNameDirectories(name);

            List<DirectoryInfo> result = new List<DirectoryInfo>();
            foreach (DirectoryInfo assemblyNameDirectory in assemblyNameDirectories)
            {
                result.AddRange(GetAssemblyDirectories(assemblyNameDirectory, version, cultureInfo, publicKeyToken));
            }
            return result.ToArray();
        }

        private DirectoryInfo[] GetAssemblyDirectories(DirectoryInfo assemblyNameDirectory, Version version, CultureInfo cultureInfo, PublicKeyToken publicKeyToken)
        {
            StringBuilder searchPattern = new StringBuilder();
            searchPattern.Append(GetSearchPattern(version));
            searchPattern.Append('_');
            searchPattern.Append(GetSearchPattern(cultureInfo));
            searchPattern.Append('_');
            searchPattern.Append(GetSearchPattern(publicKeyToken));
            return assemblyNameDirectory.GetDirectories(searchPattern.ToString());
        }

        private DirectoryInfo[] GetAssemblyNameDirectories(string name)
        {
            var result = new List<DirectoryInfo>();
            foreach (DirectoryInfo gacDir in gacDirs)
            {
                if (!gacDir.Exists) continue;

                DirectoryInfo[] gacSubDirs = gacDir.GetDirectories();
                foreach (DirectoryInfo gacSubDir in gacSubDirs)
                {
                    result.AddRange(gacSubDir.GetDirectories(GetSearchPattern(name)));
                }
            }
            return result.ToArray();
        }

        private string GetSearchPattern(object input)
        {
            if (input != null && input.ToString() != string.Empty)
            {
                return input.ToString();
            }
            else
            {
                return "*";
            }
        }
    }
}