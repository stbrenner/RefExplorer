//-----------------------------------------------------------------------------
// File:    CollectionUtil.cs
// Author:  snb
// Created: 10/03/2006
//-----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace RefExplorer.Core.Collections
{
  public static class CollectionUtil
  {
    public static void Copy<T>(IEnumerable<T> source, ICollection<T> target)
    {
      foreach (T item in source)
      {
        target.Add(item);
      }
    }

    /// <summary>
    /// Clears the target list prior to copying the values.
    /// </summary>
    public static void Replace<T>(IEnumerable<T> source, ICollection<T> target)
    {
      target.Clear();
      Copy(source, target);
    }

    public static bool Contains<T>(IEnumerable<T> list, Predicate<T> match) where T : class
    {
      T result;
      return TryGet(list, match, out result);
    }

    public static bool TryGet<T>(IEnumerable list, Predicate<T> match, out T result) where T : class
    {
      foreach (T item in list)
      {
        if (match(item))
        {
          result = item;
          return true;
        }
      }
      result = null;
      return false;
    }

    public static IList<T> GetRange<T>(IEnumerable<T> list, int index, int count)
    {
      List<T> intermediate = new List<T>(list);
      return intermediate.GetRange(index, count);
    }

    public static T[] ToArray<T>(IEnumerable<T> list)
    {
      List<T> intermediate = new List<T>(list);
      return intermediate.ToArray();
    }

    public static T[] ToArray<T>(IEnumerable list)
    {
      var result = new List<T>();
      foreach (var item in list)
      {
        result.Add((T)item);
      }
      return result.ToArray();
    }

    public static T[] ToArray<T>(IEnumerable<T> list, int index, int count)
    {
      List<T> intermediate = new List<T>(list);
      List<T> range = intermediate.GetRange(index, count);
      return range.ToArray();
    }  
  }
}