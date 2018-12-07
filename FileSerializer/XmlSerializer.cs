using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using BusinessLogic.Services;
using Reflection.Model;

namespace FileSerializer
{
    [Export(typeof(ISerializer))]
    public class XmlSerializer : ISerializer
    {
        public void Serialize(AssemblyModel sourceObject, string destination)
        { 
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(FileSerializer.Model.AssemblyModel));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

            using (XmlWriter xw = XmlWriter.Create(destination, settings))
            {
                var serializeModel = new FileSerializer.Model.AssemblyModel(sourceObject);
                dataContractSerializer.WriteObject(xw, serializeModel);
            }
        }

        public AssemblyModel Deserialize(string source)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(FileSerializer.Model.AssemblyModel));
            using (FileStream fs = new FileStream(source, FileMode.Open))
            {
                return new AssemblyModel((FileSerializer.Model.AssemblyModel)dataContractSerializer.ReadObject(fs));
            }
        }
    }
}
