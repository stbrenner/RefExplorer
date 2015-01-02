//-----------------------------------------------------------------------------
// ConfigurationFileFixture.cs
//
// Author: Stephan Brenner
// Date:   06/17/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using RefExplorer.Core.Result;
using Xunit;
using RefExplorer.Core;
using RefExplorer.Core.Configuration;
using RefExplorer.Gui;

namespace RefExplorer.Tests
{
	public class ConfigurationFileFixture
	{
		[Fact]
		public void TestBeam()
		{
            using (var instantInstaller = new InstantInstaller())
            {
                instantInstaller.Execute();

                ExplorerConfiguration createdConfig = new ExplorerConfiguration
                {
                    BaseDirectory = instantInstaller.Directory
                };
                createdConfig.AssemblyPaths.Add(new AssemblyDirectPath(@"C:\A.dll"));
                createdConfig.AssemblyPaths.Add(new AssemblyDirectoryPath(@"C:\Temp"));
                createdConfig.AssemblyPaths.Add(new AssemblySearchPath(@"C:\Bla", "Stephan.*.dll", SearchOption.TopDirectoryOnly));
                //createdConfig.OutputFile = @"C:\C.dll";

                string tempFileName = Path.GetTempFileName();
                ConfigurationFile.Save(tempFileName, createdConfig);
                ExplorerConfiguration openedConfig = ConfigurationFile.Open(tempFileName);

                Assert.True(openedConfig.AssemblyPaths[0] is AssemblyDirectPath);
                Assert.Equal(@"C:\A.dll", ((AssemblyDirectPath)openedConfig.AssemblyPaths[0]).Path);
                Assert.True(openedConfig.AssemblyPaths[1] is AssemblyDirectoryPath);
                Assert.Equal(@"C:\Temp", ((AssemblyDirectoryPath)openedConfig.AssemblyPaths[1]).Path);
                Assert.True(openedConfig.AssemblyPaths[2] is AssemblySearchPath);
                Assert.Equal(@"C:\Bla", ((AssemblySearchPath)openedConfig.AssemblyPaths[2]).Path);
                Assert.Equal("Stephan.*.dll", ((AssemblySearchPath)openedConfig.AssemblyPaths[2]).SearchPattern);
                Assert.Equal(SearchOption.TopDirectoryOnly, ((AssemblySearchPath)openedConfig.AssemblyPaths[2]).SearchOption);
                //Assert.Equal(@"C:\C.dll", openedConfig.OutputFile);
            }
		}
	  
	  [Fact]
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

      Assert.Equal(2, result.Count);
      Assert.Equal(dllFile, result[0]);
      Assert.Equal(exeFile, result[1]);
    }
	  
	  [Fact]
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

      Assert.Equal(1, result.Count);
      Assert.Equal(exeFile, result[0]);
    }
	}
}
