using System.Windows;
using BusinessLogic.Services;

namespace WpfGUI.Utilities
{
    internal class FatalErrorHandler : IFatalErrorHandler
    {
        public void HandleFatalError(string errorInfo)
        {
            MessageBox.Show($"Fatal error occurred. Application must be closed.\nError message: {errorInfo}");
            Application.Current.Shutdown(1);
        }
    }
}
