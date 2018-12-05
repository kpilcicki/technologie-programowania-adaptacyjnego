using System.Windows;
using BusinessLogic.Services;
using ServiceContract.Services;

namespace WpfGUI.Utilities
{
    public class InfoDialog : IUserInfo
    {
        public void PromptUser(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }
    }
}