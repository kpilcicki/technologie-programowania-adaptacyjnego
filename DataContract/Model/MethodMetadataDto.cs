using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataContract.Model.Enums;

namespace DataContract.Model
{
  public class MethodMetadataDto
  {
      public string MName { get; }
      public IEnumerable<TypeMetadataDto> MGenericArguments { get; }
      public Tuple<AccessLevel, AbstractENum, StaticEnum, VirtualEnum> MModifiers { get; }
      public TypeMetadataDto MReturnType { get; }
      public bool MExtension { get; }
      public IEnumerable<ParameterMetadataDto> MParameters { get; }
  }
}