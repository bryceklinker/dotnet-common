using System.Linq;
using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Steps;
using Xunit;

namespace Klinked.Gherkin.Tests.Steps
{
    public class GherkinStyleWithInlineDataSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public GherkinStyleWithInlineDataSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("I have number '(.*)'")]
        public void IHaveNumber(int number)
        {
            var numbers = _scenarioContext.Get<int[]>("numbers") ?? new int[0];
            _scenarioContext.Set("numbers", numbers.Concat(new []{number}).ToArray());
        }

        [When("I add my numbers")]
        public void IAddMyNumbers()
        {
            var numbers = _scenarioContext.Get<int[]>("numbers");
            _scenarioContext.Set("sum", numbers.Sum());
        }

        [Then("I should see a sum of '(.*)'")]
        public void IShouldSeeASumOf(int sum)
        {
            Assert.Equal(sum, _scenarioContext.Get<int>("sum"));
        }
    }
}