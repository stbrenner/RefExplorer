//-----------------------------------------------------------------------------
// File:    Program.cs
// Author:  snb
// Created: 10/03/2006
//-----------------------------------------------------------------------------

using System.Collections.Generic;
using RefExplorer.Core;
using RefExplorer.Core.Configuration;
using RefExplorer.Core.Result;
using RefExplorer.Core.CommandLine;

namespace RefExplorer.Cmd
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            CommandLineArguments parsedArgs = new CommandLineArguments();
            if (Parser.ParseArgumentsWithUsage(args, parsedArgs))
            {
                ExplorerConfiguration configuration = new ExplorerConfiguration();
                ReadFiles(configuration.AssemblyPaths, parsedArgs.Assembly);
                ReadDirectories(configuration.AssemblyPaths, parsedArgs.Directory);
                ReadSearchStrings(configuration.AssemblyPaths, parsedArgs.Search);

                ExplorerEngine engine = new ExplorerEngine(configuration);
                engine.Explore();
            }
        }

        private static void ReadDirectories(IList<AssemblyPath> paths, string[] directories)
        {
            foreach (string directory in directories)
            {
                paths.Add(new AssemblyDirectoryPath(directory));
            }
        }

        private static void ReadFiles(IList<AssemblyPath> paths, string[] assemblies)
        {
            foreach (string assembly in assemblies)
            {
                paths.Add(new AssemblyDirectoryPath(assembly));
            }
        }

        private static void ReadSearchStrings(IList<AssemblyPath> paths, string[] searchStrings)
        {
            foreach (string searchString in searchStrings)
            {
                paths.Add(new AssemblyDirectoryPath(searchString));
            }
        }
    }
}
