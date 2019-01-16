namespace FileSerializer.Exception
{
    public class WrongFilePathException : System.Exception
    {
        public WrongFilePathException()
        {
        }

        public WrongFilePathException(string message) : base(message)
        {
        }

        public WrongFilePathException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
