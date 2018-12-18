using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using FileSerializer.Mapper;
using FileSerializer.Model;

namespace FileSerializer
{
    [Export(typeof(IAssemblySerialization))]
    public class XmlSerializer : IAssemblySerialization
    {
        public void Serialize(string connectionString, AssemblyDtg assemblyDtg)
        {
            AssemblyModel assemblyToSerialize = new AssemblyModel(assemblyDtg);

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyModel));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

            using (XmlWriter xw = XmlWriter.Create(connectionString, settings))
            {
                dataContractSerializer.WriteObject(xw, assemblyToSerialize);
            }
        }

        AssemblyDtg IAssemblySerialization.Deserialize(string connectionString)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyModel));
            using (FileStream fs = new FileStream(connectionString, FileMode.Open))
            {
                return DataTransferMapper.AssemblyDtg((AssemblyModel)dataContractSerializer.ReadObject(fs));
            }
        }
    }
}
