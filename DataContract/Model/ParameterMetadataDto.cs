﻿namespace DataContract.Model
{
    public class ParameterMetadataDto
    {
        public string Name { get; set; }

        public TypeMetadataDto TypeMetadata { get; set; }

        public ParameterMetadataDto(string name, TypeMetadataDto typeMetadataDto)
        {
            Name = name;
            TypeMetadata = typeMetadataDto;
        }
    }
}