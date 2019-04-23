using System.Threading.Tasks;

namespace Klinked.Cqrs
{
    public interface ICqrsBus
    {
        Task ExecuteAsync<TCommandArgs>(TCommandArgs args);

        Task<TResult> ExecuteAsync<TArgs, TResult>(TArgs args);
        
        Task PublishAsync<TArgs>(TArgs args);
    }
}