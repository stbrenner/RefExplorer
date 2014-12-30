//-----------------------------------------------------------------------------
// DotToolFixture.cs
//
// Author: Stephan Brenner
// Date:   09/23/2006
//-----------------------------------------------------------------------------

using RefExplorer.Core.Configuration;
using NUnit.Framework;
using RefExplorer.Core;
using RefExplorer.Core.Result;

namespace RefExplorer.Tests
{
  [TestFixture]
  public class DotToolFixture
  {
    [Test]
    public void DrawFromFile()
    {
      ExplorerConfiguration configuration = new ExplorerConfiguration();
      //configuration.OutputFile = Path.Combine(Path.GetTempPath(), "ReferenceGraph.png");
      DotTool dotTool = new DotTool(configuration);
      //dotTool.DrawGraph(@"Test.dot", configuration.OutputFile);
    }
    
    [Test]
    public void DrawFromResult()
    {
      ReferenceResult result = new ReferenceResult();
      // O-###-SNB/SNB: TODO
      //result.AssemblyInfos.Add(new ReferenceInfo(@"c:\bla.dll", null));
      ExplorerConfiguration configuration = new ExplorerConfiguration();
      //configuration.OutputFile = Path.Combine(Path.GetTempPath(), "ReferenceGraph.png");
      DotTool dotTool = new DotTool(configuration);
      dotTool.DrawGraph(result);
    }  
  }
}
