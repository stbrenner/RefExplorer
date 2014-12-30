/*
 * Created by: Stephan Brenner
 * Created: 2007-09-01
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using Brenner.Common.Collections;
using Brenner.Common.CommandLine;

namespace Brenner.Common.CommandLine
{
  public class CommandRegistry
  {
    private readonly Dictionary<string, Type> commandTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);
    private readonly Dictionary<string, string> commandTags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

    public CommandRegistry(params Assembly[] assemblies)
    {
      foreach (Assembly assembly in assemblies)
      {
        Type[] types = assembly.GetTypes();
        foreach (Type type in types)
        {
          CommandAttribute[] customAttributes = (CommandAttribute[])type.GetCustomAttributes(typeof(CommandAttribute), false);
          foreach (CommandAttribute customAttribute in customAttributes)
          {
            commandTypes[customAttribute.CommandTag] = type;
            commandTags[customAttribute.CommandTag] = customAttribute.CommandTag;
          }
        }
      }
    }

    public void ExecuteCommand(params string[] args)
    {
      string commandTag;
      if (commandTags.TryGetValue(args[0], out commandTag))
      {
        string[] commandArgs = CollectionUtil.ToArray(args, 1, args.Length - 1);
        Type commandType = commandTypes[commandTag];

        ConstructorInfo constructor = commandType.GetConstructor(new Type[]{typeof(string[])});
        ICommand command = (ICommand)constructor.Invoke(new object[]{commandArgs});

        if (command.ShowHelp)
        {
          CommandHelp commandHelp = new CommandHelp(command);
          commandHelp.ShowHelp();
        }
        else
        {
          command.Execute();
        }
      }
      else
      {
        throw new NotImplementedException(string.Format("Command '{0}' is not supported.", args[0]));
      }
    }
  }
}