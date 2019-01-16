namespace DataTransferGraph.Exception
{
    public class ReadingMetadataException : System.Exception
    {
        public ReadingMetadataException()
        {
        }

        public ReadingMetadataException(string message) : base(message)
        {
        }

        public ReadingMetadataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
