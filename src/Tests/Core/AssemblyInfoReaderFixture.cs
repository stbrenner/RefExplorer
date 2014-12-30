//-----------------------------------------------------------------------------
// AssemblyInfoReaderFixture.cs
//
// Author: Stephan Brenner
// Date:   08/20/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using RefExplorer.Core.Configuration;
using NUnit.Framework;
using RefExplorer.Core.Implementation;
using RefExplorer.Core.Result;

namespace RefExplorer.Tests
{
  [TestFixture]
  public class AssemblyInfoReaderFixture
  {
    [Test]
    public void TestReading()
    {
      List<string> paths = new List<string>();
      paths.Add(GetType().Assembly.Location);
      var infoReader = new AssemblyInfoReader(new ExplorerConfiguration());
      var result = new List<ReferenceInfo>();
      // O-###-SNB/SNB: TODO
      //infoReader.Execute(result);
      Assert.AreEqual(1, result.Count);
      Assert.IsNull(result[0].Exception);
    }
  }
}
