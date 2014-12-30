//-----------------------------------------------------------------------------
// File:    CommandLineArguments.cs
// Author:  snb
// Created: 10/03/2006
//-----------------------------------------------------------------------------

using RefExplorer.Core.CommandLine;

namespace RefExplorer.Cmd
{
  internal class CommandLineArguments
  {
    [Argument(ArgumentType.Multiple, HelpText = @"Directory to search for assemblies - e.g. 'c:\temp\'")]
    public string[] Directory;
    [Argument(ArgumentType.Multiple, HelpText = @"Path to a single Assembly - e.g. 'c:\test.dll'")]
    public string[] Assembly;
    [Argument(ArgumentType.Multiple, HelpText = @"Search string which may contain placeholders - e.g. 'c:\*.dll'")]
    public string[] Search;
    [Argument(ArgumentType.AtMostOnce, HelpText = @"Title that should appear on the output.")]
    public string Title;
    [Argument(ArgumentType.Required, HelpText = @"Path ot output file - e.g. 'c:\output.png'")]
    public string Output;
  }
}