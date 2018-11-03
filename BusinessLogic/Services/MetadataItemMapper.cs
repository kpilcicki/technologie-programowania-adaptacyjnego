using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model;
using DataContract.API;
using DataContract.Model;
using Reflector.ExtensionMethods;

namespace BusinessLogic.Services
{
    public class MetadataItemMapper : IMapper<AssemblyMetadataStorage, MetadataItem>
    {
        public MetadataItem Map(AssemblyMetadataStorage objectToMAp)
        {
            Dictionary<string, MetadataItem> instances = new Dictionary<string, MetadataItem>();

            List<Relation> relations = new List<Relation>();

            // assembly
            MetadataItem assemblyItem = new MetadataItem(objectToMAp.AssemblyMetadata.Id, true); // TODO: check if has children
            instances.Add(assemblyItem.Name, assemblyItem);

            foreach (var methodMetadataDto in objectToMAp.MethodsDictionary)
            {
                MetadataItem item = MapItem(methodMetadataDto.Value);
                foreach (var relation in GetRelations(methodMetadataDto.Value))
                {
                    relations.Add(relation);
                }

                instances.Add(methodMetadataDto.Key, item);
            }

            foreach (var typeMetadataDto in objectToMAp.TypesDictionary)
            {
                MetadataItem item = MapItem(typeMetadataDto.Value);
                foreach (var relation in GetRelations(typeMetadataDto.Value))
                {
                    relations.Add(relation);
                }

                instances.Add(typeMetadataDto.Key, item);
            }

            foreach (var namespaceMetadataDto in objectToMAp.NamespacesDictionary)
            {
                MetadataItem item = MapItem(namespaceMetadataDto.Value);
                foreach (var relation in GetRelations(namespaceMetadataDto.Value))
                {
                    relations.Add(relation);
                }

                relations.Add(new Relation(assemblyItem.Name, item.Name));

                instances.Add(item.Name, item);
            }

            foreach (var parameterMetadataDto in objectToMAp.ParametersDictionary)
            {
                MetadataItem item = MapItem(parameterMetadataDto.Value);
                relations.Add(GetRelation(parameterMetadataDto.Value));
                instances.Add(parameterMetadataDto.Key, item);
            }

            foreach (var propertyMetadataDto in objectToMAp.PropertiesDictionary)
            {
                MetadataItem item = MapItem(propertyMetadataDto.Value);
                relations.Add(GetRelation(propertyMetadataDto.Value));
                instances.Add(propertyMetadataDto.Key, item);
            }

            // lets get fornicating
            foreach (var relation in relations)
            {
                instances[relation.Parent].Children.Add(instances[relation.Child]);
            }

            return assemblyItem;
        }

        private Relation GetRelation(PropertyMetadataDto value)
        {
            return new Relation(value.Id, value.TypeMetadata.Id);
        }

        private MetadataItem MapItem(PropertyMetadataDto value)
        {
            return new MetadataItem($"Property: {value.Name}", true);
        }

        private Relation GetRelation(ParameterMetadataDto value)
        {
            return new Relation(value.Id, value.TypeMetadata.Id);
        }

        private MetadataItem MapItem(ParameterMetadataDto value)
        {
            return new MetadataItem($"Parameter: {value.Name}", true);
        }

        private IEnumerable<Relation> GetRelations(NamespaceMetadataDto value)
        {
            foreach (var item in value.Types)
            {
                yield return new Relation(value.Id.AddNamespacePrefix(), item.Id);
            }
        }

        private MetadataItem MapItem(NamespaceMetadataDto value)
        {
            return new MetadataItem(value.Id.AddNamespacePrefix(), true);
        }

        private IEnumerable<Relation> GetRelations(TypeMetadataDto value)
        {
            foreach (var item in value.Constructors)
            {
                yield return new Relation(value.Id, item.Id);
            }

            foreach (var item in value.Methods)
            {
                yield return new Relation(value.Id, item.Id);
            }

            foreach (var item in value.Properties)
            {
                yield return new Relation(value.Id, item.Id);
            }

            // foreach (var item in value.Attributes)
            // {
            //    yield return new Relation(value.Id, );
            // }
            // TODO check attributes

            foreach (var item in value.GenericArguments)
            {
                yield return new Relation(value.Id, item.Id);
            }

            foreach (var item in value.ImplementedInterfaces)
            {
                yield return new Relation(value.Id, item.Id);
            }

            foreach (var item in value.NestedTypes)
            {
                yield return new Relation(value.Id, item.Id);
            }
        }

        private MetadataItem MapItem(TypeMetadataDto objectToMap)
        {
            return new MetadataItem($"{objectToMap.TypeKind.ToString().Replace("Type", string.Empty)}: {objectToMap.TypeName}", true);
        }

        private IEnumerable<Relation> GetRelations(MethodMetadataDto parent)
        {
            foreach (var argument in parent.GenericArguments)
            {
                yield return new Relation(parent.Id, argument.Id);
            }

            foreach (var parameter in parent.Parameters)
            {
                yield return new Relation(parent.Id, parameter.Id);
            }

            if (parent.ReturnType != null) yield return new Relation(parent.Id, parent.ReturnType.Id);
        }

        private MetadataItem MapItem(MethodMetadataDto objectToMap)
        {
            bool hasChildren =
                objectToMap.GenericArguments.Any() ||
                objectToMap.Parameters.Any();

            return new MetadataItem($"{objectToMap.Modifiers.Item1} " +
                                    $"{objectToMap.ReturnType?.TypeName ?? "void"} " +
                                    $"{objectToMap.Name} ",
                hasChildren);
        }
    }
}