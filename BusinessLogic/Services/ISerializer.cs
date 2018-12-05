namespace BusinessLogic.Services
{
    public interface ISerializer
    {
        void Serialize<T>(T sourceObject, string destination);

        T Deserialize<T>(string source);
    }
}
