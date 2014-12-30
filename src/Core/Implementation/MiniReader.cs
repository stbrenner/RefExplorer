//-----------------------------------------------------------------------------
// File:    MiniReader.cs
// Author:  snb
// Created: 12/26/2006
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using RefExplorer.Core.Result;
using RefExplorer.Core.Collections;

namespace RefExplorer.Core.Implementation
{
    internal class MiniReader : MarshalByRefObject
    {
        private AssemblyResult result;

        public AssemblyResult Result { get { return result; } }

        public MiniReader()
        {
        }

        public void Read(string path)
        {
            result = new AssemblyResult(path);

            try
            {
                VerifyProcessorArchitecture(path);
                Assembly assembly = Assembly.LoadFrom(path);
                ReadAssembly(assembly);
            }
            catch (Exception e)
            {
                result.Exception = e;
            }
        }

        private void VerifyProcessorArchitecture(string filePath)
        {
            var fileArchitecture = AssemblyName.GetAssemblyName(filePath).ProcessorArchitecture;
            var hostArchitecture = Assembly.GetExecutingAssembly().GetName().ProcessorArchitecture;

            if (fileArchitecture == ProcessorArchitecture.X86 && IntPtr.Size == 8)
            {
                throw new InvalidOperationException("32-bit assembly cannot be loaded by the 64-bit version of RefExplorer.");
            }
            else if (fileArchitecture == ProcessorArchitecture.Amd64 && IntPtr.Size == 4)
            {
                throw new InvalidOperationException("64-bit assembly cannot be loaded by the 32-bit version of RefExplorer.");
            }
        }

        private void ReadAssembly(Assembly assembly)
        {
            AssemblyName name = assembly.GetName();
            if (!result.Assemblies.ContainsKey(name.FullName) && !IsInTargetExcludes(name))
            {
                AssemblyInfo assemblyInfo = new AssemblyInfo(name);
                result.Assemblies.Add(name.FullName, assemblyInfo);

                if (!IsInSourceExcludes(name))
                {
                    AssemblyName[] references = assembly.GetReferencedAssemblies();
                    foreach (AssemblyName reference in references)
                    {
                        ReferenceInfo referenceInfo = ReadReference(reference);
                        if (!IsInTargetExcludes(reference))
                        {
                            assemblyInfo.References.Add(reference.FullName, referenceInfo);
                        }
                    }

                    ReadDllImport(assembly, assemblyInfo);
                }
            }
        }

        private void ReadDllImport(Assembly assembly, AssemblyInfo assemblyInfo)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(false);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is DllImportAttribute)
                        {
                            var dllImportAttribute = attribute as DllImportAttribute;

                            string name = dllImportAttribute.Value.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)
                                              ? dllImportAttribute.Value.Remove(dllImportAttribute.Value.Length - 4)
                                              : dllImportAttribute.Value;
                            var assemblyName = new AssemblyName(name);
                            if (!assemblyInfo.References.ContainsKey(name.ToLower()) &&
                                !IsInTargetExcludes(assemblyName))
                            {
                                var referenceInfo = new ReferenceInfo
                                                        {
                                                            Name = assemblyName,
                                                            Type = ReferenceType.DllImport
                                                        };
                                assemblyInfo.References[name.ToLower()] = referenceInfo;
                            }
                        }
                    }
                }
            }
        }

        private ReferenceInfo ReadReference(AssemblyName assemblyName)
        {
            if (!IsAssemblyLoaded(assemblyName))
            {
                try
                {
                    Assembly assembly = Assembly.Load(assemblyName.FullName);
                    ReadAssembly(assembly);
                }
                catch (Exception e)
                {
                    return new ReferenceInfo
                               {
                                   Name = assemblyName,
                                   Type = ReferenceType.DotNetReference,
                                   Exception = e
                               };
                }
            }
            return new ReferenceInfo
                       {
                           Name = assemblyName,
                           Type = ReferenceType.DotNetReference,
                       };
        }

        private bool IsAssemblyLoaded(AssemblyName assemblyName)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly.GetName().FullName == assemblyName.FullName)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsInTargetExcludes(AssemblyName target)
        {
            return MatchesRegexItems(target, TargetExcludes);
        }

        public List<string> TargetExcludes { get; set; }

        public List<string> SourceExcludes { get; set; }

        private static bool MatchesRegexItems(AssemblyName target, IEnumerable<string> list)
        {
            return CollectionUtil.Contains(list, delegate(string item)
            {
                string trimmedItem = item.Trim();
                if (string.IsNullOrEmpty(trimmedItem)) return false;
                string excaptedItem = Regex.Escape(trimmedItem);
                string regex = string.Format("^{0}$", excaptedItem.Replace(@"\*", ".*"));
                return Regex.IsMatch(target.Name, regex);
            });
        }

        private bool IsInSourceExcludes(AssemblyName source)
        {
            return MatchesRegexItems(source, SourceExcludes);
        }

    }
}