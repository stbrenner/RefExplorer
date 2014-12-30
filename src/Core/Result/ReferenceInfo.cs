//-----------------------------------------------------------------------------
// AssemblyInfo.cs
//
// Author: Stephan Brenner
// Date:   08/06/2006
//-----------------------------------------------------------------------------

using System;
using System.Reflection;

namespace RefExplorer.Core.Result
{
    [Serializable]
    public class ReferenceInfo
    {
        public AssemblyName Name { get; set; }
        public ReferenceType Type { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            if (Exception == null)
            {
                return "Success";
            }
            else
            {
                return Exception.ToString();
            }
        }
    }
}
