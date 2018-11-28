﻿using System.Runtime.Serialization;

namespace DataContract.Model
{
    [DataContract(IsReference = true)]
    public class ParameterModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeModel Type { get; set; }
    }
}