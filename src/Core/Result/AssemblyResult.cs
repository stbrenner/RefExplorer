//~----------------------------------------------------------------------------
// File:    AssemblyResult.cs
// Author:  Stephan Brenner
// Created: 04/18/2007
//----------------------------------------------------------------------------~

using System;
using System.Collections.Generic;

namespace RefExplorer.Core.Result
{
  [Serializable]
  public class AssemblyResult
  {
    private readonly string path;
    private Exception exception;
    private readonly IDictionary<string, AssemblyInfo> assemblies = new Dictionary<string, AssemblyInfo>();

    public AssemblyResult(string path)
    {
      this.path = path;
    }

    public IDictionary<string, AssemblyInfo> Assemblies { get { return assemblies; } }
    public string Path { get { return path; } }
    public Exception Exception { get { return exception; } set { exception = value; } }
  }
}
