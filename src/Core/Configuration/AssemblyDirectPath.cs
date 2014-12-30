//-----------------------------------------------------------------------------
// AssemblyDirectPath.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace RefExplorer.Core.Configuration
{
  public class AssemblyDirectPath : AssemblyPath
  {
    private string path;

    public AssemblyDirectPath()
    { }

    public AssemblyDirectPath(string path)
    {
      this.path = path;
    }

    public string Path { get { return path; } set { path = value; } }

    public override IList<string> Resolve()
    {
      List<string> result = new List<string>();
      result.Add(path);
      return result;
    }

    public override string ToString()
    {
      return path;
    }
  }
}
