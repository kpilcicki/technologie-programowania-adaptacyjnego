using Reflection.Model;

namespace BusinessLogic.Services
{
    public interface ISerializer
    {
        void Serialize(AssemblyModel sourceObject, string destination);

        AssemblyModel Deserialize(string source);
    }
}
