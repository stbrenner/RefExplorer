/*
 * Created by: Stephan Brenner
 * Created: 06/04/2007
 */

using System;

namespace RefExplorer.Core.CommandLine
{
  public abstract class CommandBase<TArgs> : ICommand where TArgs : new()
  {
    private readonly TArgs args = new TArgs();
    private readonly string tag;
    private readonly bool showHelp = false;

    public CommandBase(string[] rawArgs)
    {
      CommandAttribute[] customAttributes = (CommandAttribute[])GetType().GetCustomAttributes(typeof(CommandAttribute), false);
      tag = customAttributes[0].CommandTag;

      if (Parser.ParseHelp(rawArgs))
      {
        showHelp = true;
      }
      else
      {
        Parser.ParseArguments(rawArgs, args);
      }
    }

    public TArgs Args { get { return args; } }
    public Type ArgsType{get{return typeof (TArgs);}}
    public string Tag { get { return tag; } }
    public bool ShowHelp { get { return showHelp; } }

    public abstract void Execute();
  }
}