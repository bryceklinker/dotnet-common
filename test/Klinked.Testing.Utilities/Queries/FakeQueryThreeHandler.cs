using System.Threading.Tasks;
using Klinked.Cqrs.Queries;

namespace Klinked.Testing.Utilities.Queries
{
    public class FakeQueryThreeArgs
    {
        public byte[] Result { get; }

        public FakeQueryThreeArgs(byte[] result)
        {
            Result = result;
        }
    }
    
    public class FakeQueryThreeHandler : IQueryHandler<FakeQueryThreeArgs, byte[]>
    {
        public Task<byte[]> Execute(FakeQueryThreeArgs args)
        {
            return Task.FromResult(args.Result);
        }
    }
}