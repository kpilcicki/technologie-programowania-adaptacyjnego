namespace DataTransferGraph.Exception
{
    public class SavingMetadataException : System.Exception
    {
        public SavingMetadataException()
        {
        }

        public SavingMetadataException(string message) : base(message)
        {
        }

        public SavingMetadataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
