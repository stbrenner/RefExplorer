//-----------------------------------------------------------------------------
// AssemblyInfoReaderFixture.cs
//
// Author: Stephan Brenner
// Date:   08/20/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using RefExplorer.Core.Configuration;
using Xunit;
using RefExplorer.Core.Implementation;
using RefExplorer.Core.Result;

namespace RefExplorer.Tests
{
    
    public class AssemblyInfoReaderFixture
    {
        [Fact]
        public void TestReading()
        {
            var config = new ExplorerConfiguration();
            config.AssemblyPaths.Add(new AssemblyDirectPath(GetType().Assembly.Location));

            var infoReader = new AssemblyInfoReader(config);
            var result = new Dictionary<string, AssemblyResult>();

            infoReader.Execute(result);
            Assert.Equal(1, result.Count);
        }
    }
}
