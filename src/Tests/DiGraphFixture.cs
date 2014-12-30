//-----------------------------------------------------------------------------
// DiGraphFixture.cs
//
// Author: Stephan Brenner
// Date:   09/25/2006
//-----------------------------------------------------------------------------

using System.IO;
using RefExplorer.Core.Configuration;
using NUnit.Framework;
using RefExplorer.Core;
using RefExplorer.Core.Result;

namespace RefExplorer.Tests
{
  [TestFixture]
  public class DiGraphFixture
  {
    [Test]
    public void TestWrite()
    {
      ReferenceResult result = new ReferenceResult();
      // O-###-SNB/SNB: TODO
      //result.AssemblyInfos.Add(new ReferenceInfo(@"c:\bla.dll", null));

      using (MemoryStream stream = new MemoryStream())
      {
        DiGraph diGraph = new DiGraph(result, new ExplorerConfiguration());
        diGraph.Write(stream);

        Assert.AreEqual(306, stream.Length);
      }
    }
  }
}
