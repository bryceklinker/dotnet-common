using System.Threading.Tasks;

namespace Klinked.Cqrs.Queries
{
    public interface IQueryHandler<in TArgs, TResult>
    {
        Task<TResult> ExecuteAsync(TArgs args);
    }
}