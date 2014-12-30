using System.Collections.Generic;
using RefExplorer.Core.Result;

namespace RefExplorer.Core
{
  public class ReferenceResult
  {
    private readonly IDictionary<string, AssemblyResult> _references = new Dictionary<string, AssemblyResult>();
    public readonly string ImageFileName = "RefExp-Image.png";

    public IDictionary<string, AssemblyResult> References { get { return _references; } }
  }
}
