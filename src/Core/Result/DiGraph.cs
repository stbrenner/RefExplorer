//-----------------------------------------------------------------------------
// DiGraph.cs
//
// Author: Stephan Brenner
// Date:   09/20/2006
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using RefExplorer.Core.Configuration;

namespace RefExplorer.Core.Result
{
    public class DiGraph
    {
        private ReferenceResult result;
        private ExplorerConfiguration configuration;

        public DiGraph(ReferenceResult result, ExplorerConfiguration configuration)
        {
            this.result = result;
            this.configuration = configuration;
        }

        public void Write(Stream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("digraph G {");
            writer.WriteLine("graph [fontname=\"Tahoma\",fontsize=10.0];");
            writer.WriteLine("node [shape=box,fontname=\"Tahoma\",fontsize=10.0];");
            writer.WriteLine("edge [fontname=\"Tahoma\",fontsize=10.0];");
            writer.WriteLine("edge [color=midnightblue];");

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (AssemblyResult assemblyResult in result.References.Values)
            {
                foreach (AssemblyInfo assemblyInfo in assemblyResult.Assemblies.Values)
                {
                    CollectData(dictionary, assemblyInfo);
                }
            }


            foreach (string value in dictionary.Values)
            {
                writer.Write(value);
            }

            writer.WriteLine("}");
            writer.Flush();
        }

        private void CollectData(Dictionary<string, string> dictionary, AssemblyInfo assemblyInfo)
        {
            foreach (KeyValuePair<string, ReferenceInfo> reference in assemblyInfo.References)
            {
                if (reference.Value.Type == ReferenceType.DotNetReference || configuration.ShowNativeReferences)
                {
                    StringBuilder value = new StringBuilder();
                    WriteAssemblyName(value, assemblyInfo.Name);
                    value.Append(" -> ");
                    WriteAssemblyName(value, reference.Value.Name);
                    WriteStyle(value, reference.Value);
                    value.AppendLine(";");
                    string key = string.Format("{0} -> {1}", assemblyInfo.Name.FullName, reference.Value.Name.FullName);
                    dictionary[key] = value.ToString();
                }
            }
        }

        private void WriteStyle(StringBuilder value, ReferenceInfo info)
        {
            if (info.Exception != null)
            {
                value.Append(" [label=\"Error\",fontcolor=red,color=red]");
            }
        }

        private void WriteAssemblyName(StringBuilder value, AssemblyName assemblyName)
        {
            value.AppendFormat("\"{0}\\n{1}\"", assemblyName.Name.Replace(@"\", @"\\"), assemblyName.Version);
        }
    }
}
