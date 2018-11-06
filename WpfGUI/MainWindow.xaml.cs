using System.Windows;
using BusinessLogic.Services;
using BusinessLogic.ViewModel;
using DataContract.API;
using FileLogger;
using Reflection.AssemblyLoader;

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
            return new MainViewModel(
                new FileDialog(),
                new Reflector(logger),
                new MetadataItemMapper());
        }
    }
}