using System.ComponentModel.DataAnnotations;
namespace scb_H60L01.Models
{
    public class Person
    {
        private static long CurrentPersonNum = 1;
        private static List<Person> PersonList = new List<Person>();

        public long PersonId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name must be between 1 and 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Display(Name = "Country")]
        public ValidCountry Country { get; set; }

        [Display(Name = "Validated?")]
        public bool Validated { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public enum ValidCountry
        {
            Canada,
            Albania,
            Denmark,
            France,
            Hungary,
            Spain,
            Turkey,
            Zaire
        }

        public Person(long personId, string firstName, string lastName, ValidCountry country, bool validated, string email)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Validated = validated;
            Email = email;
        }

        public Person(string firstName, string lastName, ValidCountry country, bool validated, string email)
        {
            PersonId = CurrentPersonNum++;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Validated = validated;
            Email = email;
        }

        public Person(long personId)
        {
            PersonId = personId;
        }

        public Person() { }

        public static List<Person> GetPersons()
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
