using System.Threading.Tasks;

namespace Klinked.Cqrs.Commands
{
    public interface ICommandHandler<in TArgs>
    {
        Task ExecuteAsync(TArgs args);
    }
}