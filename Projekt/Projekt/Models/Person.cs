namespace Projekt.Models
{
    public class Person
    {
        public int ID;
        private string _FirstName;
        public string FirstName { get { return _FirstName;  } }
        private string _LastName;
        public string LastName { get { return _LastName; } }
        private DateTime _Birthdate;
        public int Age { get { return DateTime.Now.Year - _Birthdate.Year; } }
        public enum Genders
        {
            m = 0,
            w = 1,
            d = 2
        }
        private Genders _Gender;
        public Genders Gender { get { return _Gender; } }
        public Person (string firstName, string lastName, DateTime birthdate, Genders gender)
        {
            _FirstName = firstName;
            _LastName = lastName;
            if (ValidBirthdate(birthdate)) { _Birthdate = birthdate; }
            _Gender = gender;
        }
        public void ChangeFirstName(string firstName)
        {
            _FirstName = firstName;
        }
        public void ChangeLastName(string lastName)
        {
            _LastName = lastName;
        }
        public void ChangeBirthdate(DateTime birthdate)
        {
            if (ValidBirthdate(birthdate))
            {
                _Birthdate = birthdate;
            }
        }
        public void ChangeGender(Genders gender)
        {
            _Gender = gender;
        }
        private bool ValidBirthdate(DateTime birthdate)
        {
            if (birthdate > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(birthdate), "Birthdate must be in the past.");
            }
            else
            {
                return true;
            }
        }
    }
}
