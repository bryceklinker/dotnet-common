using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Queries;

namespace Klinked.Cqrs.Tests.Fakes.Queries
{
    public class FakeRetryQueryArgs
    {
        public int TimesExecuted { get; private set; }

        public int TimesToFail { get; }
        
        public int[] Result { get; }

        public FakeRetryQueryArgs(int timesToFail, int[] result)
        {
            TimesToFail = timesToFail;
            Result = result;
        }

        public bool ShouldFail()
        {
            return TimesToFail > TimesExecuted++;
        }
    }

    public class FakeRetryQueryHandler : IQueryHandler<FakeRetryQueryArgs, int[]>
    {
        public Task<int[]> ExecuteAsync(FakeRetryQueryArgs queryArgs)
        {
            if (queryArgs.ShouldFail())
                throw new Exception();

            return Task.FromResult(queryArgs.Result);
        }
    }
}