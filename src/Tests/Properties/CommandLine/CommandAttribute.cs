/*
 * Created by: Stephan Brenner
 * Created: Samstag, 1. September 2007
 */

using System;

namespace Brenner.Common.CommandLine
{
  [AttributeUsage(AttributeTargets.Class)]
  public class CommandAttribute : Attribute
  {
    private readonly string commandTag;

    public CommandAttribute(string commandTag)
    {
      this.commandTag = commandTag;
    }

    public string CommandTag { get { return commandTag; } }  }
}