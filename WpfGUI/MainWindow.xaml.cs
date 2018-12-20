using System.Windows;
using BusinessLogic;
using BusinessLogic.ViewModel;
using WpfGUI.Utilities;

namespace WpfGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var x = InitializeViewModel();
            DataContext = x;
        }

        private MainViewModel InitializeViewModel()
        {
            return Composer.GetComposedMainViewModel(
                new InfoDialog(),
                new FileDialog(),
                new FatalErrorHandler());
        }
    }
}