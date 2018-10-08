using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Klinked.Gherkin.Sample.Common
{
    public class Fixture
    {
        private readonly List<Person> _people;

        public Person[] People => _people.ToArray();

        public int LaughRating { get; private set; }
        
        public Fixture()
        {
            _people = new List<Person>();
        }

        public void AddPerson(string firstName, string lastName)
        {
            _people.Add(new Person {FirstName = firstName, LastName = lastName});
        }

        public void TellJoke(string firstName, string lastName)
        {
            var person = GetPerson(firstName, lastName);
            if (person.FirstName == "Jerry")
                LaughRating = 10;
            else
                LaughRating = 0;
        }

        private Person GetPerson(string firstName, string lastName)
        {
            return _people.Where(p => p.FirstName == firstName)
                .SingleOrDefault(p => p.LastName == lastName);
        }
    }

    [CollectionDefinition(Name)]
    public class FixtureCollection : ICollectionFixture<Fixture>
    {
        public const string Name = "Fixture";
    }
}