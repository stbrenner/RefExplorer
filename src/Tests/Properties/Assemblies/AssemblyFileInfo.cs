using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Brenner.Common.Assemblies
{
    public class AssemblyFileInfo : FileSystemInfo
    {
        private static readonly Regex regex = new Regex(@"GAC(_(?<TargetPlatform>\w*))?\\(?<AssemblyName>[\w\-\.]*)\\(v(?<FrameworkVersion>[0-9\.]*)_)?(?<Version>[0-9\.]*)_(?<Culture>[a-zA-Z\-]*)_(?<PublicKey>[\w]*)$");
        private FileInfo fileInfo;
        private string assemblyName;
        private Version version;
        private CultureInfo cultureInfo;
        private PublicKeyToken publicKeyToken;
        private TargetPlatform targetPlatform;
        private Version frameworkVersion;

        private AssemblyFileInfo()
        {
        }

        public override void Delete()
        {
            throw new NotSupportedException();
        }

        public override string Name { get { return fileInfo.Name; } }
        public DirectoryInfo Directory { get { return fileInfo.Directory; } }
        public override bool Exists { get { return fileInfo.Exists; } }
        public override string FullName { get { return fileInfo.FullName; } }
        public string AssemblyName { get { return assemblyName; } }
        public Version Version { get { return version; } }
        public CultureInfo CultureInfo { get { return cultureInfo; } }
        public PublicKeyToken PublicKeyToken { get { return publicKeyToken; } }

        public TargetPlatform TargetPlatform
        {
            get { return targetPlatform; }
        }

        public Version FrameworkVersion
        {
            get { return frameworkVersion; }
        }

        public static AssemblyFileInfo FromGacFile(FileInfo gacFileInfo)
        {
            DirectoryInfo versionDirectory = gacFileInfo.Directory;
            Match match = regex.Match(versionDirectory.FullName);

            string frameworkVersionString = match.Groups["FrameworkVersion"].Value;
            string versionString = match.Groups["Version"].Value;

            return new AssemblyFileInfo
                       {
                           fileInfo = gacFileInfo,
                           targetPlatform = ParseTargetPlatform(match.Groups["TargetPlatform"].Value),
                           assemblyName = match.Groups["AssemblyName"].Value,
                           frameworkVersion = string.IsNullOrEmpty(frameworkVersionString) ? null : new Version(frameworkVersionString),
                           version = string.IsNullOrEmpty(versionString) ? null : new Version(versionString),
                           cultureInfo = new CultureInfo(match.Groups["Culture"].Value),
                           publicKeyToken = new PublicKeyToken(match.Groups["PublicKey"].Value)
                       };
        }

        private static TargetPlatform ParseTargetPlatform(string value)
        {
            if (string.Compare(value, "32", true) == 0)
            {
                return TargetPlatform.X32;
            }

            if (string.Compare(value, "64", true) == 0)
            {
                return TargetPlatform.X64;
            }

            if (string.Compare(value, "MSIL", true) == 0)
            {
                return TargetPlatform.MSIL;
            }

            return TargetPlatform.Unknown;
        }
    }
}