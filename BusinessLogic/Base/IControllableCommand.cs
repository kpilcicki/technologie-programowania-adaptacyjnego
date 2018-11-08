using System.Windows.Input;

namespace BusinessLogic.Base
{
    public interface IControllableCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
