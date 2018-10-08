using Klinked.Gherkin.Sample.Common;
using Klinked.Gherkin.Steps;
using Xunit;

namespace Klinked.Gherkin.Sample.Steps
{
    public class AdvancedSteps
    {
        private readonly Fixture _fixture;

        public AdvancedSteps(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Given("I have a person named (.*) (.*)")]
        public void IHaveAPersonNamed(string firstName, string lastName)
        {
            _fixture.AddPerson(firstName, lastName);
        }

        [When("(.*) (.*) tells a joke")]
        public void PersonTellsAJoke(string firstName, string lastName)
        {
            _fixture.TellJoke(firstName, lastName);
        }

        [Then("I laugh mucho")]
        public void ILaughMucho()
        {
            Assert.Equal(10, _fixture.LaughRating);
        }
        
        [Then("I hear crickets")]
        public void IHearCrickets()
        {
            Assert.Equal(0, _fixture.LaughRating);
        }
    }
}