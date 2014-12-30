//-----------------------------------------------------------------------------
// ProgramFixture.cs
//
// Author: snb
// Date:   10/03/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using NUnit.Framework;
using RefExplorer.Cmd;

namespace RefExplorer.Tests.Cmd
{
  [TestFixture]
  public class ProgramFixture
  {
    [Test]
    public void TestResolfingArgs()
    {
      List<string> args = new List<string>();
      args.Add("/Output:bla");
      args.Add("/Assembly:hudel");

      Program.Main(args.ToArray());
    }
  }
}