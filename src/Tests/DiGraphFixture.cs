//-----------------------------------------------------------------------------
// DiGraphFixture.cs
//
// Author: Stephan Brenner
// Date:   09/25/2006
//-----------------------------------------------------------------------------

using System.IO;
using RefExplorer.Core.Configuration;
using Xunit;
using RefExplorer.Core;
using RefExplorer.Core.Result;
using RefExplorer.Gui;
using RefExplorer.Core.Implementation;

namespace RefExplorer.Tests
{

    public class DiGraphFixture
    {
        [Fact]
        public void TestWrite()
        {
            using (var instantInstaller = new InstantInstaller())
            {
                instantInstaller.Execute();

                var config = new ExplorerConfiguration
                {
                    BaseDirectory = instantInstaller.Directory
                };
                config.AssemblyPaths.Add(new AssemblyDirectPath(GetType().Assembly.Location));

                var infoReader = new AssemblyInfoReader(config);
                var result = new ReferenceResult();
                infoReader.Execute(result.References);

                using (MemoryStream stream = new MemoryStream())
                {
                    DiGraph diGraph = new DiGraph(result, new ExplorerConfiguration());
                    diGraph.Write(stream);

                    Assert.Equal(645, stream.Length);
                }
            }
        }
    }
}
