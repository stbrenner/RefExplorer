/*
 * Created by: Stephan Brenner
 * Created: 2008-02-17
 */

using NUnit.Framework;
using RefExplorer.Core.Implementation;

namespace RefExplorer.Tests.Core
{
  [TestFixture]
  public class MiniReaderFixture
  {
    [Test]
    public void TestXXX()
    {
      var miniReader = new MiniReader();
      miniReader.Read(@"C:\Windows\assembly\gac_msil\Enteo.BlServer.Common\6.1.1.2418__2aa8ae3ddce4beba\Enteo.BlServer.Common.dll");
    }
  }
}