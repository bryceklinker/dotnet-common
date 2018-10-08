using System.Threading.Tasks;
using Klinked.Cqrs.Queries;

namespace Klinked.Cqrs.Tests.Fakes.Queries
{
    public class FakeQueryArgs
    {
        public int[] Result { get; }

        public FakeQueryArgs(int[] result)
        {
            Result = result;
        }
    }
    
    public class FakeQueryHandler : IQueryHandler<FakeQueryArgs, int[]>
    {
        public Task<int[]> Execute(FakeQueryArgs args)
        {
            return Task.FromResult(args.Result);
        }
    }
}