using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Steps;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Tests.Steps
{
    public class GherkinStyleWithDifferentParameterOrderSteps
    {
        private readonly ITestOutputHelper _output;
        private readonly CollectionFixture _fixture;
        private readonly ScenarioContext _scenarioContext;

        public GherkinStyleWithDifferentParameterOrderSteps(ITestOutputHelper output, CollectionFixture fixture, ScenarioContext scenarioContext)
        {
            _output = output;
            _fixture = fixture;
            _scenarioContext = scenarioContext;
        }

        [Then("I don't care about order")]
        public void IDontCareAboutOrder()
        {
            _scenarioContext.Set("FixtureId", _fixture.Id);
            _scenarioContext.Set("TestOutput", _output);
        }
    }
}