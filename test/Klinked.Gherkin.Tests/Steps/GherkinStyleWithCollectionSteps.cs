using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Steps;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Tests.Steps
{
    public class GherkinStyleWithCollectionSteps
    {
        private readonly CollectionFixture _fixture;
        private readonly ITestOutputHelper _output;
        private readonly ScenarioContext _scenarioContext;

        public GherkinStyleWithCollectionSteps(CollectionFixture fixture, ITestOutputHelper output, ScenarioContext scenarioContext)
        {
            _fixture = fixture;
            _output = output;
            _scenarioContext = scenarioContext;
        }

        [Given("I need the fixture")]
        public void INeedTheFixture()
        {
            _scenarioContext.Set("FixtureId", _fixture.Id);
        }

        [When("I need fixture and output")]
        public void INeedFixtureAndOutput()
        {
            _scenarioContext.Set("FixtureId", _fixture.Id);
            _scenarioContext.Set("TestOutput", _output);
        }
    }
}