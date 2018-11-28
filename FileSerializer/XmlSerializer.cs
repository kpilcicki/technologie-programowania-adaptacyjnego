using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using DataContract.API;

namespace FileSerializer
{
    public class XmlSerializer : ISerializer
    {
        public void Serialize<T>(T sourceObject, string destination)
        { 
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

            using (XmlWriter xw = XmlWriter.Create(destination, settings))
            {
                dataContractSerializer.WriteObject(xw, sourceObject);
            }
        }

        public T Deserialize<T>(string source)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
            using (FileStream fs = new FileStream(source, FileMode.Open))
            {
                return (T)dataContractSerializer.ReadObject(fs);
            }
        }
    }
}
