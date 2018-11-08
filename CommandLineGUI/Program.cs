using BusinessLogic.Services;
using BusinessLogic.ViewModel;
using DataContract.API;
using FileLogger;
using Reflection.AssemblyLoader;

namespace CommandLineGUI
{
    internal class Program
    {
        internal static void Main()
        {
            MainView main = new MainView(InitializeMainViewModel());
            main.Display();
        }

        private static MainViewModel InitializeMainViewModel()
        {
            ILogger logger = new Logger();

            return new MainViewModel(
                new ConsoleFilePathGetter(),
                new Reflector(logger),
                new MetadataItemMapper());
        }
    }
}
