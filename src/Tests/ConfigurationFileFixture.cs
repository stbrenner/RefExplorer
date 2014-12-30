//-----------------------------------------------------------------------------
// ConfigurationFileFixture.cs
//
// Author: Stephan Brenner
// Date:   06/17/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using RefExplorer.Core.Result;
using NUnit.Framework;
using RefExplorer.Core;
using RefExplorer.Core.Configuration;

namespace RefExplorer.Tests
{
	[TestFixture]
	public class ConfigurationFileFixture
	{
		[Test]
		public void TestBeam()
		{
			ExplorerConfiguration createdConfig = new ExplorerConfiguration();
			createdConfig.AssemblyPaths.Add(new AssemblyDirectPath(@"C:\A.dll"));
			createdConfig.AssemblyPaths.Add(new AssemblyDirectoryPath(@"C:\Temp"));
      createdConfig.AssemblyPaths.Add(new AssemblySearchPath(@"C:\Bla", "Stephan.*.dll", SearchOption.TopDirectoryOnly));
      //createdConfig.OutputFile = @"C:\C.dll";

			string tempFileName = Path.GetTempFileName();
			ConfigurationFile.Save(tempFileName, createdConfig);
			ExplorerConfiguration openedConfig = ConfigurationFile.Open(tempFileName);

      Assert.IsTrue(openedConfig.AssemblyPaths[0] is AssemblyDirectPath);
      Assert.AreEqual(@"C:\A.dll", ((AssemblyDirectPath)openedConfig.AssemblyPaths[0]).Path);
      Assert.IsTrue(openedConfig.AssemblyPaths[1] is AssemblyDirectoryPath);
      Assert.AreEqual(@"C:\Temp", ((AssemblyDirectoryPath)openedConfig.AssemblyPaths[1]).Path);
      Assert.IsTrue(openedConfig.AssemblyPaths[2] is AssemblySearchPath);
      Assert.AreEqual(@"C:\Bla", ((AssemblySearchPath)openedConfig.AssemblyPaths[2]).Path);
      Assert.AreEqual("Stephan.*.dll", ((AssemblySearchPath)openedConfig.AssemblyPaths[2]).SearchPattern);
      Assert.AreEqual(SearchOption.TopDirectoryOnly, ((AssemblySearchPath)openedConfig.AssemblyPaths[2]).SearchOption);
      //Assert.AreEqual(@"C:\C.dll", openedConfig.OutputFile);
		}
	  
	  [Test]
	  public void TestDirectoryResolve()
	  {
      string directory = Path.Combine(Path.GetTempPath(), "TestDirectoryResolve");
      Directory.CreateDirectory(directory);
      string dllFile = Path.Combine(directory, "File.dll");
      File.Create(dllFile);
      string exeFile = Path.Combine(directory, "File.exe");
      File.Create(exeFile);
	    
	    AssemblyDirectoryPath assemblyDirectory = new AssemblyDirectoryPath(directory);
      IList<string> result = assemblyDirectory.Resolve();

      Assert.AreEqual(2, result.Count);
      Assert.AreEqual(dllFile, result[0]);
      Assert.AreEqual(exeFile, result[1]);
    }
	  
	  [Test]
	  public void TestSearchResolve()
	  {
      string directory = Path.Combine(Path.GetTempPath(), "TestSearchResolve");
      Directory.CreateDirectory(directory);
      string dllFile = Path.Combine(directory, "File.dll");
      File.Create(dllFile);
      string exeFile = Path.Combine(directory, "File.exe");
      File.Create(exeFile);

      AssemblySearchPath assemblyDirectory = new AssemblySearchPath(directory, "*.exe", SearchOption.TopDirectoryOnly);
      IList<string> result = assemblyDirectory.Resolve();

      Assert.AreEqual(1, result.Count);
      Assert.AreEqual(exeFile, result[0]);
    }
	}
}
