using BusinessLogic.ViewModel;
using CommandLineGUI.Utilities;
using FileLogger;
using FileSerializer;
using Reflection;

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
            return new MainViewModel(
                new Reflector(),
                new ConsoleUserInfo(),
                new ConsoleFilePathGetter(),
                new XmlSerializer(),
                new Logger());
        }
    }
}
