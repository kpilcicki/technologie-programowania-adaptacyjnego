﻿using System.Runtime.Serialization;

namespace Reflection.Model
{
    [DataContract(IsReference = true)]
    public class ParameterModel
    {
        [DataMember] public string Name { get; set; }

        [DataMember] public TypeModel Type { get; set; }

        public ParameterModel(string name, TypeModel typeModel)
        {
            Name = name;
            Type = typeModel;
        }
    }
}