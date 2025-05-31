using Projekt.Utilities;

namespace Projekt.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public int Age { get { return DateTime.Now.Year - Birthdate.Year; } }
        public enum Genders
        {
            m = 0,
            w = 1,
            d = 2
        }
        public Genders Gender { get; private set; }
        public Person(string firstName, string lastName, DateTime birthdate, Genders gender)
        {
            Validator.ValidateString(firstName, nameof(firstName));
            FirstName = firstName;
            Validator.ValidateString(lastName, nameof(lastName));
            LastName = lastName;
            ValidateBirthdate(birthdate);
            Birthdate = birthdate;
            Validator.ValidateEnumValue(gender, nameof(gender));
            Gender = gender;
        }
        protected Person()
        {
            FirstName = "Firstname";
            LastName = "Lastname";
        }
        public void ChangeFirstName(string firstName)
        {
            Validator.ValidateString(firstName, nameof(firstName));
            FirstName = firstName;
        }
        public void ChangeLastName(string lastName)
        {
            Validator.ValidateString(lastName, nameof(lastName));
            LastName = lastName;
        }
        public void ChangeBirthdate(DateTime birthdate)
        {
            ValidateBirthdate(birthdate);
            Birthdate = birthdate;
        }
        public void ChangeGender(Genders gender)
        {
            Validator.ValidateEnumValue(gender, nameof(gender));
            Gender = gender;
        }
        private void ValidateBirthdate(DateTime birthdate)
        {
            if (birthdate > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(birthdate), $"{nameof(birthdate)} must be in the past.");
            }
        }
    }
}
