using System.Threading.Tasks;
using Klinked.Gherkin.Scenarios;
using Xunit;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Sample
{
    public class SimpleFeature : Feature
    {
        public SimpleFeature(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        [Scenario("Can Add Numbers")]
        public async Task CanAddNumbers()
        {
            await Given("I have the number 5");
            await Given("I have the number 5");
            await When("I add my numbers");
            await Then("I should see 10");
        }
    }
}