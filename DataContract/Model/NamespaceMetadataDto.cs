using System;
using System.Collections.Generic;
using System.Linq;

namespace DataContract.Model
{
  public class NamespaceMetadataDto
  {
      public string MNamespaceName { get; set; }
      public IEnumerable<TypeMetadataDto> MTypes { get; set; }
  }
}