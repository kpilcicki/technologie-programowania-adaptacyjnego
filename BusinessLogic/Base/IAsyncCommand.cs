using System.Threading.Tasks;

namespace BusinessLogic.Base
{
    public interface IAsyncCommand : IControllableCommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
