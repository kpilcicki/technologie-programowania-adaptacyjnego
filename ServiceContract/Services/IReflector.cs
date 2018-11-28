using DataContract.Model;

namespace ServiceContract.Services
{
    public interface IReflector
    {
        AssemblyModel ReflectDll(string dllFilePath);
    }
}
