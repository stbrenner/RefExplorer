//-----------------------------------------------------------------------------
// AssemblyPathResolver.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using RefExplorer.Core.Configuration;

namespace RefExplorer.Core.Implementation
{
  internal class AssemblyPathResolver
  {
    private ICollection<AssemblyPath> references;

    public AssemblyPathResolver(ICollection<AssemblyPath> references)
    {
      this.references = references;
    }
    
    public IList<string> Execute()
    {
      List<string> result = new List<string>();
      
      foreach (AssemblyPath reference in references)
      {
        result.AddRange(reference.Resolve());
      }

      return result;
    }
  }
}