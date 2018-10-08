using System.Threading.Tasks;
using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Steps;

namespace Klinked.Gherkin.Tests.Steps
{
    public class GherkinSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public GherkinSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("I put (.*) in the scenario context")]
        public void IPutDataInScnearioContext(string data)
        {
            _scenarioContext.Set("given", data);
        }

        [When("I perform some action using parameter (.*)")]
        public void IPerformSomeActionUsingParameter(string parameter)
        {
            _scenarioContext.Set("when", parameter);
        }

        [Then("I can use context in then with parameter (.*)")]
        public void ICanUseContextInThenWithParameter(string parameter)
        {
            _scenarioContext.Set("then", parameter);
        }

        [Given("something async")]
        [When("something async")]
        [Then("something async")]
        public Task Something()
        {
            return Task.Delay(500);
        }

        [Given("I have many parameters (.*), (.*), (.*)")]
        public void IHaveManyParameters(string first, string second, string third)
        {
            _scenarioContext.Set("first", first);
            _scenarioContext.Set("second", second);
            _scenarioContext.Set("third", third);
        }

        [Given("this is defined twice")]
        public void FirstDefinition()
        {
            
        }

        [Given("this is defined twice")]
        public void SecondDefinition()
        {
            
        }
    }
}