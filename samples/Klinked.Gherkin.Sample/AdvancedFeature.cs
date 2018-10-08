using System.Threading.Tasks;
using Klinked.Gherkin.Sample.Common;
using Klinked.Gherkin.Scenarios;
using Xunit;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Sample
{
    [Collection(FixtureCollection.Name)]
    public class AdvancedFeature : Feature
    {
        public AdvancedFeature(ITestOutputHelper output, Fixture fixture) 
            : base(output, fixture)
        {
        }

        [Fact]
        [Scenario("Mucho Laughter")]
        public async Task MuchoLaughter()
        {
            await Given("I have a person named Jerry Seinfeld");
            await When("Jerry Seinfeld tells a joke");
            await Then("I laugh mucho");
        }
        
        [Fact]
        [Scenario("Crickets")]
        public async Task Crickets()
        {
            await Given("I have a person named Jimmy Jackson");
            await When("Jimmy Jackson tells a joke");
            await Then("I hear crickets");
        }
    }
}