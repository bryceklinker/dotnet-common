using System;
using System.Threading.Tasks;
using Klinked.Gherkin.Scenarios;
using Xunit;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Tests
{
    public class CollectionFixture
    {
        public Guid Id { get; } = Guid.NewGuid();
    }

    [CollectionDefinition(Name)]
    public class Collection : ICollectionFixture<CollectionFixture>
    {
        public const string Name = "Collection";
    }
    
    [Collection(Collection.Name)]
    public class GherkinStyleWithCollectionTests : Feature
    {
        private readonly ITestOutputHelper _output;
        private readonly CollectionFixture _fixture;

        public GherkinStyleWithCollectionTests(ITestOutputHelper output, CollectionFixture fixture)
            : base(output, fixture)
        {
            _output = output;
            _fixture = fixture;
        }
        
        [Fact]
        public async Task ShouldPassFixtureToSteps()
        {
            await Given("I need the fixture");
            Assert.Equal(_fixture.Id, ScenarioContext.Get<Guid>("FixtureId"));
        }
        
        [Fact]
        public async Task ShouldPassFixtureToStepsThatRequireOutputAndFixture()
        {
            await When("I need fixture and output");
            Assert.Equal(_output, ScenarioContext.Get<ITestOutputHelper>("TestOutput"));
            Assert.Equal(_fixture.Id, ScenarioContext.Get<Guid>("FixtureId"));
        }
        
        [Fact]
        public async Task ShouldNotRequireParametersToBeInSpecificOrderForCreatingSteps()
        {
            await Then("I don't care about order");
            Assert.Equal(_output, ScenarioContext.Get<ITestOutputHelper>("TestOutput"));
            Assert.Equal(_fixture.Id, ScenarioContext.Get<Guid>("FixtureId"));
        }
    }
}