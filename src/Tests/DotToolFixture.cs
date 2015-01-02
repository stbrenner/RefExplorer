//-----------------------------------------------------------------------------
// DotToolFixture.cs
//
// Author: Stephan Brenner
// Date:   09/23/2006
//-----------------------------------------------------------------------------

using RefExplorer.Core.Configuration;
using Xunit;
using RefExplorer.Core;
using RefExplorer.Core.Result;
using RefExplorer.Core.Implementation;
using System.Collections.Generic;
using RefExplorer.Gui;

namespace RefExplorer.Tests
{
    public class DotToolFixture
    {
        [Fact]
        public void DrawFromFile()
        {
            ExplorerConfiguration configuration = new ExplorerConfiguration();
            //configuration.OutputFile = Path.Combine(Path.GetTempPath(), "ReferenceGraph.png");
            DotTool dotTool = new DotTool(configuration);
            //dotTool.DrawGraph(@"Test.dot", configuration.OutputFile);
        }

        [Fact]
        public void DrawFromResult()
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

                DotTool dotTool = new DotTool(config);
                dotTool.DrawGraph(result);
            }
        }
    }
}
