using System.Windows;
using BusinessLogic.ViewModel;
using FileLogger;
using FileSerializer;
using Reflection;
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
            return new MainViewModel(
                new Reflector(),
                new InfoDialog(),
                new FileDialog(),
                new XmlSerializer(),
                new Logger());
        }
    }
}