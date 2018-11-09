using BusinessLogic.ViewModel;
using DataContract.API;
using FileLogger;

namespace CommandLineGUI
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            MainView mainView = new MainView(GetViewModel());
            mainView.Display();
        }

        private static MainViewModel GetViewModel()
        {
            ILogger logger = new Logger();
            return new MainViewModel(new ConsoleFilePathGetter(), logger);
        }
    }
}
