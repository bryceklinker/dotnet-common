using System.Linq;
using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Steps;
using Xunit;

namespace Klinked.Gherkin.Sample.Steps
{
    public class SimpleSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public SimpleSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("I have the number (.*)")]
        public void IHaveNumber(int value)
        {
            var values = (_scenarioContext.Get<int[]>("numbers") ?? new int[0])
                .Concat(new []{value});
            _scenarioContext.Set("numbers", values.ToArray());
        }

        [When("I add my numbers")]
        public void IAddMyNumbers()
        {
            var numbers = _scenarioContext.Get<int[]>("numbers");
            var sum = Calculator.Add(numbers);
            _scenarioContext.Set("sum", sum);
        }

        [Then("I should see (.*)")]
        public void IShouldSeeNumber(int sum)
        {
            Assert.Equal(sum, _scenarioContext.Get<int>("sum"));
        }
    }
}