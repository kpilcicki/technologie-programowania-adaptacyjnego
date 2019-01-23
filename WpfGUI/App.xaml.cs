using System.Windows;
using BusinessLogic;

namespace WpfGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Composer.Instance.Dispose();
        }
    }
}
