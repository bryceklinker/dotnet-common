using System.Threading.Tasks;

namespace Klinked.Cqrs
{
    public interface ICqrsBus
    {
        Task Execute<TCommandArgs>(TCommandArgs args);

        Task<TResult> Execute<TArgs, TResult>(TArgs args);
        
        Task Publish<TArgs>(TArgs args);
    }
}