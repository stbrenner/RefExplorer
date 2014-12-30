//-----------------------------------------------------------------------------
// AssemblyInfoReader.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using RefExplorer.Core.Configuration;
using RefExplorer.Core.Result;

namespace RefExplorer.Core.Implementation
{
    internal class AssemblyInfoReader
    {
        private readonly ExplorerConfiguration configuration;

        public AssemblyInfoReader(ExplorerConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Execute(IDictionary<string, AssemblyResult> result)
        {
            var pathResolver = new AssemblyPathResolver(configuration.AssemblyPaths);
            ICollection<string> paths = pathResolver.Execute();
            
            foreach (string path in paths)
            {
                AssemblyResult assemblyResult = ReadAssembly(path);
                result.Add(path, assemblyResult);
            }
        }

        private AssemblyResult ReadAssembly(string path)
        {
            AppDomain dummyDomain = null;
            try
            {
                var setup = new AppDomainSetup
                                {
                                    ApplicationBase = Path.GetDirectoryName(path), 
                                    ConfigurationFile = string.Format("{0}.config", path)
                                };

                dummyDomain = AppDomain.CreateDomain("DummyDomain", AppDomain.CurrentDomain.Evidence, setup);
                Type readerType = typeof(MiniReader);
                var miniReader = (MiniReader)dummyDomain.CreateInstanceFromAndUnwrap(readerType.Assembly.Location, readerType.FullName);
                miniReader.TargetExcludes = configuration.TargetExcludes;
                miniReader.SourceExcludes = configuration.SourceExcludes;
                miniReader.Read(path);
                return miniReader.Result;
            }
            catch(Exception e)
            {
                return new AssemblyResult(path)
                           {
                               Exception = e
                           };   
            }
            finally
            {
                AppDomain.Unload(dummyDomain);
            }
        }
    }
}
