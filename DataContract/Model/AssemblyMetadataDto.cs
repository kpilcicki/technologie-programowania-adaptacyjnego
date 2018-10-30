using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DataContract.Model
{
  public class AssemblyMetadataDto
  {
      public string MName { get; set; }
      public IEnumerable<NamespaceMetadataDto> MNamespaces { get; set; }
  }
}