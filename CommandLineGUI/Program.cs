using BusinessLogic.ViewModel;
using DataContract.API;
using FileLogger;

namespace CommandLineGUI
{
    internal class Program
    {
        internal static void Main(string[] args)
        {

        }

        private MainViewModel InitializeMainViewModel()
        {
            ILogger logger = new Logger();
            return new MainViewModel(new ConsoleFilePathGetter(), logger);
        }
    }
}
