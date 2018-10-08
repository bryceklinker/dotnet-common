using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Queries;

namespace Klinked.Cqrs.Tests.Fakes.Queries
{
    public class FakeQueryTwoArgs
    {
        public Guid Result { get; }

        public FakeQueryTwoArgs(Guid result)
        {
            Result = result;
        }
    }

    public class FakeQueryTwoHandler : IQueryHandler<FakeQueryTwoArgs, Guid>
    {
        public Task<Guid> Execute(FakeQueryTwoArgs args)
        {
            return Task.FromResult(args.Result);
        }
    }
}