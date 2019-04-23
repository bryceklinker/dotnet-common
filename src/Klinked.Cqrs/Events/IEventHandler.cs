using System.Threading.Tasks;

namespace Klinked.Cqrs.Events
{
    public interface IEventHandler<in TArgs>
    {
        Task HandleAsync(TArgs args);
    }
}