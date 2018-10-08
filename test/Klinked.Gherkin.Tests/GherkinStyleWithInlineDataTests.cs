using System.Threading.Tasks;
using Klinked.Gherkin.Scenarios;
using Xunit;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Tests
{
    public class GherkinStyleWithInlineDataTests : Feature
    {
        public GherkinStyleWithInlineDataTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Theory]
        [InlineData(5, 4, 9)]
        [InlineData(6, 1, 7)]
        [InlineData(9, 12, 21)]
        [Scenario("Add numbers")]
        public async Task AddNumbers(int first, int second, int sum)
        {
            await Given($"I have number '{first}'");
            await Given($"I have number '{second}'");
            await When("I add my numbers");
            await Then($"I should see a sum of '{sum}'");
        }
    }
}