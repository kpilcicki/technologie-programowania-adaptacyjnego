namespace BusinessLogic.Model
{
    internal class ParameterMetadata
    {
        public string Name { get; }

        public TypeMetadata Metadata { get; }

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            Metadata = typeMetadata;
        }
    }
}