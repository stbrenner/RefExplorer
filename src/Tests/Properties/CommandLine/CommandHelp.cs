/*
 * Created by: Stephan Brenner
 * Created: 2007-09-17
 */

using System;
using System.Text;
using Brenner.Common.CommandLine;

namespace Brenner.Common.CommandLine
{
  internal class CommandHelp
  {
    private readonly ICommand command;

    public CommandHelp(ICommand command)
    {
      this.command = command;
    }

    public void ShowHelp()
    {
      StringBuilder output = new StringBuilder();
      output.AppendLine(string.Format("CmdHelper {0} <Arguments>", command.Tag));
      output.AppendLine();
      output.AppendLine("Arguments:");
      output.Append(Parser.ArgumentsUsage(command.ArgsType));
      Console.WriteLine(output);
    }
  }
}