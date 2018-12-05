using DataContract.Model;
using Reflection.Model;

namespace ServiceContract.Services
{
    public interface IReflector
    {
        AssemblyModel ReflectDll(string dllFilePath);
    }
}
