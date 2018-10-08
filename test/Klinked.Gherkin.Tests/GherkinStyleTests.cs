using System;
using System.Threading.Tasks;
using Klinked.Gherkin.Scenarios;
using Klinked.Gherkin.Tests.Fakes;
using Xunit;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Tests
{
    public class GherkinFeature : Feature
    {
        private readonly FakeTestOutputHelper _output;

        public GherkinFeature(ITestOutputHelper output)
            : base(new FakeTestOutputHelper(output))
        {
            _output = Output as FakeTestOutputHelper;
        }
        
        [Fact]
        public async Task KeepsContextFromGiven()
        {
            await Given("I put 3453 in the scenario context");
            Assert.Equal("3453", ScenarioContext.Get<string>("given"));
        }
        
        [Fact]
        public async Task KeepsContextFromWhen()
        {
            await When("I perform some action using parameter one");
            Assert.Equal("one", ScenarioContext.Get<string>("when"));
        }
        
        [Fact]
        public async Task KeepsContextFromThen()
        {
            await Then("I can use context in then with parameter hello");
            Assert.Equal("hello", ScenarioContext.Get<string>("then"));
        }
        
        [Fact]
        [Scenario("Logs Scenario Name")]
        public async Task LogsScenarioBeingRun()
        {
            await Given("I put something in the scenario context");
            Assert.Contains(_output.Messages, m => m.Contains("Logs Scenario Name"));
        }
        
        [Fact]
        public async Task ShouldAllowExecutionOfAsyncSteps()
        {
            await Given("something async");
            await When("something async");
            await Then("something async");
        }
        
        [Fact]
        public async Task ShouldAllowMultipleParametersForGiven()
        {
            await Given("I have many parameters one, two, three");
            Assert.Equal("one", ScenarioContext.Get<string>("first"));
            Assert.Equal("two", ScenarioContext.Get<string>("second"));
            Assert.Equal("three", ScenarioContext.Get<string>("third"));
        }
        
        [Fact]
        public async Task ShouldThrowExceptionWhenStepNotFound()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await Given("this step is not defined"));
            Assert.Contains("this step is not defined", exception.Message);
        }
        
        [Fact]
        public async Task ShouldThrowExceptionWhenMultipleStepsFound()
        {
            var exception =
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await Given("this is defined twice"));
            Assert.Contains("this is defined twice", exception.Message);
        }
        
        [Fact]
        public async Task ShouldOutputScenarioSteps()
        {
            await Given("something async");
            await When("something async");
            await Then("something async");
            Assert.Contains("Given something async", _output.AllText);
            Assert.Contains("When something async", _output.AllText);
            Assert.Contains("Then something async", _output.AllText);
        }
        
        [Fact]
        public async Task ShouldReplaceValueInContext()
        {
            await Given("I put 3453 in the scenario context");
            await Given("I put 674 in the scenario context");
            Assert.Equal("674", ScenarioContext.Get<string>("given"));
        }
    }
}