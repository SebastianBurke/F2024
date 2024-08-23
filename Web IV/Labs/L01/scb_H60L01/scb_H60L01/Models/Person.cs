namespace scb_H60L01.Models
{
    public class Person
    {
        private static long CurrentPersonNum = 1;
        private static List<Person> PersonList = new List<Person>();

        public long PersonId;
        public string FirstName;
        public string LastName;
        public string Country;
        public bool Validated;
        public string Email;

        public Person (long personId, string firstName, string lastName, string country, bool validated, string email)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Validated = validated;
            Email = email;
        }
        public Person(string firstName, string lastName, string country, bool validated, string email)
        {
            PersonId = CurrentPersonNum++;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Validated = validated;
            Email = email;
        }

        public Person (long personId)
        {
            PersonId = personId;
        }

        public Person () { }

        public List<Person> GetPersons()
        {
            return PersonList;
        }

        public bool AddPerson()
        {
            PersonList.Add(this);
            return true;
        }

    }
}
