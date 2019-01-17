using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using DataTransferGraph.Exception;
using DataTransferGraph.Model;
using DataTransferGraph.Services;
using FileSerializer.Exception;
using FileSerializer.Mapper;
using FileSerializer.Model;

namespace FileSerializer
{
    [Export(typeof(IAssemblyPersist))]
    public class XmlSerializer : IAssemblyPersist
    {
        public void Persist(AssemblyDtg assemblyDtg)
        {
            try
            {
                string filePath = GetValidFilePath();

                AssemblyModel assemblyToSerialize = new AssemblyModel(assemblyDtg);

                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyModel));
                XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

                using (XmlWriter xw = XmlWriter.Create(filePath, settings))
                {
                    dataContractSerializer.WriteObject(xw, assemblyToSerialize);
                }

            }
            catch (WrongFilePathException e)
            {
                throw new SavingMetadataException(e.Message, e);
            }
            catch (System.Exception e)
            {
                throw new SavingMetadataException(e.Message, e);
            }
        }

        public AssemblyDtg Read()
        {
            try
            {
                string filePath = GetValidFilePath();
                
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(AssemblyModel));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return DataTransferMapper.AssemblyDtg((AssemblyModel) dataContractSerializer.ReadObject(fs));
                }
            }
            catch (WrongFilePathException e)
            {
                throw new ReadingMetadataException(e.Message, e);
            }
            catch (System.Exception e)
            {
                throw new ReadingMetadataException(e.Message, e);
            }

        }

        private string GetValidFilePath()
        {
            string filePath = ConfigurationManager.AppSettings["filePathToDataSource"];
            if (string.IsNullOrEmpty(filePath))
            {
                throw new WrongFilePathException("Provided file path is empty or null");
            }
            if (!filePath.EndsWith(".xml", StringComparison.InvariantCulture))
            {
                throw new WrongFilePathException(
                    $"Provided file path {filePath} is invalid (unknown extension). Provide valid .xml file path");
            }

            return filePath;
        }
    }
}
