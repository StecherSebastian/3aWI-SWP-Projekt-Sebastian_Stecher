using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Person
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "FirstName is required.")]
        [Length(2, 50, ErrorMessage = "FirstName has to be at least 3 Characters long up to a maximum of 50.")]
        public string FirstName { get; private set; }
        [Required(ErrorMessage = "LastName is required.")]
        [Length(2, 50, ErrorMessage = "LastName has to be at least 3 Characters long up to a maximum of 50.")]
        public string LastName { get; private set; }
        [Required(ErrorMessage = "BirthDate is required.")]
        public DateTime BirthDate { get; private set; }
        public int Age { get { return DateTime.Now.Year - BirthDate.Year; } }
        public enum Genders
        {
            m = 0,
            w = 1,
            d = 2
        }
        [Required(ErrorMessage = "Gender is required.")]
        public Genders Gender { get; private set; }
        public Person(string firstName, string lastName, DateTime birthdate, Genders gender)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthdate;
            Gender = gender;
        }
        protected Person()
        {
            FirstName = "Firstname";
            LastName = "Lastname";
        }
        public void ChangeFirstName(string firstName)
        {
            Utilities.Validator.ValidateString(firstName, nameof(firstName));
            FirstName = firstName;
        }
        public void ChangeLastName(string lastName)
        {
            Utilities.Validator.ValidateString(lastName, nameof(lastName));
            LastName = lastName;
        }
        public void ChangeBirthdate(DateTime birthdate)
        {
            ValidateBirthdate(birthdate);
            BirthDate = birthdate;
        }
        public void ChangeGender(Genders gender)
        {
            Utilities.Validator.ValidateEnumValue(gender, nameof(gender));
            Gender = gender;
        }
        private void ValidateBirthdate(DateTime birthdate)
        {
            if (birthdate >= DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(birthdate), $"{nameof(birthdate)} must be in the past and cannot be today.");
            }
        }
    }
}
