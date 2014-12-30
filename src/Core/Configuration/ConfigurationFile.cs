//-----------------------------------------------------------------------------
// ConfigurationFile.cs
//
// Author: Stephan Brenner
// Date:   06/17/2006
//-----------------------------------------------------------------------------

using System.IO;
using System.Xml.Serialization;

namespace RefExplorer.Core.Configuration
{
  public class ConfigurationFile
  {
    private ConfigurationFile()
    {}
    
    public static ExplorerConfiguration Open(string path)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(ExplorerConfiguration));
      using (FileStream fs = new FileStream(path, FileMode.Open))
      {
        return (ExplorerConfiguration)serializer.Deserialize(fs);
      }
    }

    public static void Save(string path, ExplorerConfiguration configuration)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(ExplorerConfiguration));
      using (TextWriter writer = new StreamWriter(path))
      {
        serializer.Serialize(writer, configuration);
      }		  
    }
		
  }
}
