/*
 * Created by: Stephan Brenner
 * Created: 2007-11-22
 */

using System.Reflection;
using System.Text;

namespace RefExplorer.Core.Assemblies
{
  public class PublicKeyToken
  {
    private readonly string value;

    public PublicKeyToken(string value)
    {
      this.value = value;
    }

    public PublicKeyToken(AssemblyName assemblyName)
    {
      byte[] pkt = assemblyName.GetPublicKeyToken();
      StringBuilder publicKeyToken = new StringBuilder();
      foreach (byte item in pkt)
      {
        publicKeyToken.Append(item.ToString("x"));
      }
      value = publicKeyToken.ToString();
    }

    public override string ToString()
    {
      return value;
    }
  }
}