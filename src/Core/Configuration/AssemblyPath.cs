//-----------------------------------------------------------------------------
// AssemblyPath.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RefExplorer.Core.Configuration
{
    [XmlInclude(typeof(AssemblyDirectoryPath))]
    [XmlInclude(typeof(AssemblyDirectPath))]
    [XmlInclude(typeof(AssemblySearchPath))]
    public abstract class AssemblyPath
    {
        public abstract IList<string> Resolve();
    }
}