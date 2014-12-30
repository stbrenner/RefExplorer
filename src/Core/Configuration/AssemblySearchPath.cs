//-----------------------------------------------------------------------------
// AssemblySearchPath.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace RefExplorer.Core.Configuration
{
  public class AssemblySearchPath : AssemblyPath
  {
    private SearchOption searchOption;
    private string path;
    private string searchPattern;

    public AssemblySearchPath()
    {}

    public AssemblySearchPath(string path, string searchPattern, SearchOption searchOption)
    {
      Path = path;
      SearchPattern = searchPattern;
      SearchOption = searchOption;
    }

    public string Path { get { return path; } set { path = value; } }
    public SearchOption SearchOption { get { return searchOption; } set { searchOption = value; } }
    public string SearchPattern { get { return searchPattern; } set { searchPattern = value; } }

    public override IList<string> Resolve()
    {
      List<string> result = new List<string>();
      result.AddRange(Directory.GetFiles(Path, SearchPattern, SearchOption));
      return result;
    }

    public override string ToString()
    {
      return string.Format("{0}; {1}; {2}", path, searchPattern, searchOption);
    }
  }
}

