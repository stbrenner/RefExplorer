//~----------------------------------------------------------------------------
// File:    AssemblyInfo.cs
// Author:  Stephan Brenner
// Created: 04/18/2007
//----------------------------------------------------------------------------~

using System;
using System.Collections.Generic;
using System.Reflection;

namespace RefExplorer.Core.Result
{
  [Serializable]
  public class AssemblyInfo
  {
    private readonly AssemblyName name;
    private readonly Exception exception;
    private readonly Dictionary<string, ReferenceInfo> references = new Dictionary<string, ReferenceInfo>();
    private readonly string path;

    public AssemblyInfo(string path, Exception exception)
    {
      this.path = path;
      this.exception = exception;
    }

    public AssemblyInfo(AssemblyName name, Exception exception)
    {
      this.name = name;
      this.exception = exception;
    }
    
    public AssemblyInfo(AssemblyName name)
    {
      this.name = name;
    }

    public Exception Exception { get { return exception; } }
    public Dictionary<string, ReferenceInfo> References { get { return references; } }
    public string Path { get { return path; } }
    public AssemblyName Name { get { return name; } }
  }
}
