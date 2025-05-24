using Projekt.Models;
using ModelsUnitTests.Helpers;
namespace ModelsUnitTests
{
    public class PersonUnitTests
    {
        private Person _Person = null!;
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0)]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0)]
        [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0)]
        public void CreatePerson_WithValidInputs_ReturnsPersonWithCorrectAttributes(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            DateTime birthdate = TestUtils.ParseBirthdate(birthdateString);
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            Assert.That(_Person.FirstName, Is.EqualTo(firstName));
            Assert.That(_Person.LastName, Is.EqualTo(lastName));
            Assert.That(_Person.Gender, Is.EqualTo(gender));
            Assert.That(_Person.Age, Is.EqualTo(TestUtils.CalculateAge(birthdate)));
        }
        [Test]
        [TestCase(null, "Stecher", "10.05.2008", 0)]
        [TestCase(" ", "Fleisch", "21.11.2007", 0)]
        [TestCase("Sebastian", null, "10.05.2008", 0)]
        [TestCase("Florian", " ", "21.11.2007", 0)]
        public void CreatePerson_WithInvalidFirstName_ThrowsArgumentException(string? firstName, string? lastName, string birthdateString, Person.Genders gender)
        {
            var ex = Assert.Throws<ArgumentException>(() => TestUtils.CreatePerson(firstName, lastName, birthdateString, gender));
            Assert.That(ex.ParamName, Is.AnyOf("firstName", "lastName"));
        }
        [Test]
        [TestCase("Leonie-Sophie", "Stecher", "12.01.2030", 0)]
        public void CreatePerson_WithInvalidLastName_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreatePerson(firstName, lastName, birthdateString, gender));
            Assert.That(ex.ParamName, Is.EqualTo("birthdate"));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 3)]
        [TestCase("Sebastian", "Stecher", "10.05.2008", -1)]
        public void CreatePerson_WithInvalidGender_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreatePerson(firstName, lastName, birthdateString, gender));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0)]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0)]
        [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0)]
        public void GetAge_Today_ReturnsCorrectCurrentAge(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            DateTime birthdate = TestUtils.ParseBirthdate(birthdateString);
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            Assert.That(_Person.Age, Is.EqualTo(TestUtils.CalculateAge(birthdate)));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, "Max")]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, "Mathias")]
        [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, "Annabelle")]
        public void ChangeFirstname_WhenSet_ReturnNewFirstname(string firstName, string lastName, string birthdateString, Person.Genders gender, string newFirstName)
        {
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            _Person.ChangeFirstName(newFirstName);
            Assert.That(_Person.FirstName, Is.EqualTo(newFirstName));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, null)]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, " ")]
        public void ChangeFirstname_WithInvalidValue_ThrowsArgumentException(string firstName, string lastName, string birthdateString, Person.Genders gender, string? newFirstName)
        {
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            Assert.Throws<ArgumentException>(() => _Person.ChangeFirstName(newFirstName));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, "Hechenberger")]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, "Egger")]
        [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, "Müller")]
        public void ChangeLastname_WhenSet_ReturnNewLastname(string firstName, string lastName, string birthdateString, Person.Genders gender, string newLastName)
        {
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            _Person.ChangeLastName(newLastName);
            Assert.That(_Person.LastName, Is.EqualTo(newLastName));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, null)]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, " ")]
        public void ChangeLastname_WithInvalidValue_ThrowsArgumentexception(string firstName, string lastName, string birthdateString, Person.Genders gender, string? newLastName)
        {
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            Assert.Throws<ArgumentException>(() => _Person.ChangeLastName(newLastName));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, "12.05.2008")]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, "21.10.2007")]
        [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, "20.04.2006")]
        public void ChangeBirthdate_WhenSet_RecalculatesAge(string firstName, string lastName, string birthdateString, Person.Genders gender, string newBirthdateString)
        {
            DateTime newBirthdate = TestUtils.ParseBirthdate(newBirthdateString);
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            _Person.ChangeBirthdate(newBirthdate);
            Assert.That(_Person.Age, Is.EqualTo(TestUtils.CalculateAge(newBirthdate)));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, "12.05.2030")]
        public void ChangeBirthdate_WithInvalidValue_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdateString, Person.Genders gender, string newBirthdateString)
        {
            DateTime newBirthdate = TestUtils.ParseBirthdate(newBirthdateString);
            _Person = TestUtils.CreatePerson(firstName,lastName, birthdateString, gender);
            Assert.Throws<ArgumentOutOfRangeException>(() => _Person.ChangeBirthdate(newBirthdate));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, 0)]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, 2)]
        [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, 1)]
        public void ChangeGender_WhenSet_ReturnNewGender(string firstName, string lastName, string birthdateString, Person.Genders gender, Person.Genders newGender)
        {
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            _Person.ChangeGender(newGender);
            Assert.That(_Person.Gender, Is.EqualTo(newGender));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 0, 3)]
        [TestCase("Florian", "Fleisch", "21.11.2007", 0, -1)]
        public void ChangeGender_WithInvalidValue_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdateString, Person.Genders gender, Person.Genders newGender)
        {
            _Person = TestUtils.CreatePerson(firstName, lastName, birthdateString, gender);
            Assert.Throws<ArgumentOutOfRangeException>(() => _Person.ChangeGender(newGender));
        }
    }
}