using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RefExplorer.Gui
{
    public class ConfigFile<T>
    {
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(T));
        private readonly string fullName = Path.Combine(Application.LocalUserAppDataPath, "config.xml");

        public string FullName
        {
            get { return fullName; }
        }

        public T Load()
        {
            using (var stream = new FileStream(fullName, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
        
        public void Save(T config)
        {
            using (var stream = new FileStream(fullName, FileMode.Create))
            {
                serializer.Serialize(stream, config);
            }
        }

        public bool Exists()
        {
            return File.Exists(fullName);
        }
    }
}
