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
            DataContext = InitializeViewModel();
        }

        private MainViewModel InitializeViewModel()
        {
            return Composer.Instance.GetComposedMainViewModel(
                new InfoDialog(),
                new FileDialog(),
                new FatalErrorHandler());
        }
    }
}