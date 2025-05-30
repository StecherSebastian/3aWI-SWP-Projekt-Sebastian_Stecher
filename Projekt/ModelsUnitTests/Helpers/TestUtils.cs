using Projekt.Models;
using System.Globalization;

namespace ModelsUnitTests.Helpers
{
    internal class TestUtils
    {
        private const string DateFormat = "dd.MM.yyyy";
        public static DateTime ParseBirthdate(string birthdateString)
        {
            return DateTime.ParseExact(birthdateString, DateFormat, CultureInfo.InvariantCulture);
        }
        public static int CalculateAge(DateTime birthdate)
        {
            return DateTime.Now.Year - birthdate.Year;
        }
        public static Person CreatePerson(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            DateTime birthdate = ParseBirthdate(birthdateString);
            return new Person(firstName, lastName, birthdate, gender);
        }
        public static Student CreateStudent(string firstName, string lastName, string birthdateString, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track)
        {
            DateTime birthdate = ParseBirthdate(birthdateString);
            return new Student(firstName, lastName, birthdate, gender, schoolclass, track);
        }
        public static Room CreateRoom(string name, int size)
        {
            return new Room(name, size);
        }
    }
}
