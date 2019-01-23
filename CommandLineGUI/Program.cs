using BusinessLogic;
using BusinessLogic.ViewModel;
using CommandLineGUI.Utilities;

namespace CommandLineGUI
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            MainView mainView = new MainView(InitializeViewModel());
            mainView.Display();
        }

        private static MainViewModel InitializeViewModel()
        {
            return Composer.Instance.GetComposedMainViewModel(
                new ConsoleUserInfo(),
                new ConsoleFilePathGetter(),
                new ConsoleFatalErrorHandler());
        }
    }
}
