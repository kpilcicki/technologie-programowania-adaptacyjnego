using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataContract.Model;
using Reflector.ExtensionMethods;

namespace Reflector.Model
{
    internal class PropertyLoader
    {
        internal static IEnumerable<PropertyMetadataDto> EmitProperties(IEnumerable<PropertyInfo> props, AssemblyMetadataStorage metaStore)
        {
            return (from prop in props
                where prop.GetGetMethod().IsVisible() || prop.GetSetMethod().IsVisible()
                select new PropertyMetadataDto

                {
                    Name = prop.Name,
                    TypeMetadata = TypeLoader.LoadTypeMetadataDto(prop.PropertyType, metaStore)
                }).ToList();
        }
    }
}