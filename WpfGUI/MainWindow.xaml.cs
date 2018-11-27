using System.Windows;
using BusinessLogic.ViewModel;
using DataContract.API;
using FileLogger;
using WpfGUI.Utilities;

namespace WpfGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = InitializeViewModel();
        }

        private MainViewModel InitializeViewModel()
        {
            ILogger logger = new Logger();
            return new MainViewModel(new FileDialog(), new Logger(), new InfoDialog());
        }
    }
}