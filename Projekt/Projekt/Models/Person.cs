namespace Projekt.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public int Age
        {
            get
            {
                int year = 0;
                if (BirthDate.HasValue)
                {
                    year = BirthDate.Value.Year;
                }
                return DateTime.Now.Year - year;
            }
        }

        public enum Genders
        {
            m = 0,
            w = 1,
            d = 2
        }
        public Genders? Gender { get; private set; }
        protected Person(string? firstName, string? lastName, DateTime? birthdate, Genders? gender)
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
