using System.Windows;
using BusinessLogic.Services;

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