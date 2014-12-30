//-----------------------------------------------------------------------------
// AssemblyDirectoryPath.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace RefExplorer.Core.Configuration
{
  public class AssemblyDirectoryPath : AssemblyPath
  {
    private string path;

    public AssemblyDirectoryPath()
    {}
    
    public AssemblyDirectoryPath(string path)
    {
      this.path = path;
    }

    public string Path { get { return path; } set { path = value; } }

    public override IList<string> Resolve()
    {
      List<string> result = new List<string>();
      result.AddRange(Directory.GetFiles(path, "*.dll"));
      result.AddRange(Directory.GetFiles(path, "*.exe"));
      return result;
    }

    public override string ToString()
    {
      return path;
    }
  }
}
