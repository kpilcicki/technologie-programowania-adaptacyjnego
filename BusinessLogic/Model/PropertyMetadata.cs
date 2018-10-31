using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BusinessLogic.Model
{
  internal class PropertyMetadata
  {
    internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
    {
      return from prop in props
             where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
             select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
    }

    #region private
    private string _name;
    private TypeMetadata _typeMetadata;

    private PropertyMetadata(string propertyName, TypeMetadata propertyType)
    {
      _name = propertyName;
      _typeMetadata = propertyType;
    }
    #endregion

  }
}