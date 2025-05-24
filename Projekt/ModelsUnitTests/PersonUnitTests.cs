using Projekt.Models;
using ModelsUnitTests.Helpers;
namespace ModelsUnitTests
{
    public class PersonUnitTests
    {
        private Person _Person = null!;
        [SetUp]
        public void Setup()
        {
            _Person = new Person("Sebastian", "Stecher", new DateTime(2008, 5, 10), 0);
        }
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
        public void CreatePerson_WithInvalidFirstAndLastName_ThrowsArgumentException(string? firstName, string? lastName, string birthdateString, Person.Genders gender)
        {
            var ex = Assert.Throws<ArgumentException>(() => TestUtils.CreatePerson(firstName, lastName, birthdateString, gender));
            Assert.That(ex.ParamName, Is.AnyOf("firstName", "lastName"));
        }
        [Test]
        [TestCase("Leonie-Sophie", "Stecher", "12.01.2030", 0)]
        public void CreatePerson_WithInvalidBirthdate_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreatePerson(firstName, lastName, birthdateString, gender));
            Assert.That(ex.ParamName, Is.EqualTo("birthdate"));
        }
        [Test]
        [TestCase("Sebastian", "Stecher", "10.05.2008", 3)]
        [TestCase("Sebastian", "Stecher", "10.05.2008", -1)]
        public void CreatePerson_WithInvalidGender_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdateString, Person.Genders gender)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreatePerson(firstName, lastName, birthdateString, gender));
            Assert.That(ex.ParamName, Is.EqualTo("gender"));
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
        [TestCase("Lukas")]
        public void ChangeFirstname_WhenSet_ReturnNewFirstname(string newFirstName)
        {
            _Person.ChangeFirstName(newFirstName);
            Assert.That(_Person.FirstName, Is.EqualTo(newFirstName));
        }
        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        public void ChangeFirstname_SetInvalidValue_ThrowsArgumentException(string? newFirstName)
        {
            var ex = Assert.Throws<ArgumentException>(() => _Person.ChangeFirstName(newFirstName));
            Assert.That(ex.ParamName, Is.EqualTo("firstName"));
        }
        [Test]
        [TestCase("Hechenberger")]
        public void ChangeLastname_WhenSet_ReturnNewLastname(string newLastName)
        {
            _Person.ChangeLastName(newLastName);
            Assert.That(_Person.LastName, Is.EqualTo(newLastName));
        }
        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        public void ChangeLastname_SetInvalidValue_ThrowsArgumentexception(string? newLastName)
        {
            var ex = Assert.Throws<ArgumentException>(() => _Person.ChangeLastName(newLastName));
            Assert.That(ex.ParamName, Is.EqualTo("lastName"));
        }
        [Test]
        [TestCase("20.04.2006")]
        public void ChangeBirthdate_WhenSet_RecalculatesAge(string newBirthdateString)
        {
            DateTime newBirthdate = TestUtils.ParseBirthdate(newBirthdateString);
            _Person.ChangeBirthdate(newBirthdate);
            Assert.That(_Person.Age, Is.EqualTo(TestUtils.CalculateAge(newBirthdate)));
        }
        [Test]
        [TestCase("12.05.2030")]
        public void ChangeBirthdate_SetInvalidValue_ThrowsArgumentOutOfRangeException(string newBirthdateString)
        {
            DateTime newBirthdate = TestUtils.ParseBirthdate(newBirthdateString);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _Person.ChangeBirthdate(newBirthdate));
            Assert.That(ex.ParamName, Is.EqualTo("birthdate"));
        }
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void ChangeGender_WhenSet_ReturnNewGender(Person.Genders newGender)
        {
            _Person.ChangeGender(newGender);
            Assert.That(_Person.Gender, Is.EqualTo(newGender));
        }
        [Test]
        [TestCase(3)]
        [TestCase(-1)]
        public void ChangeGender_SetInvalidValue_ThrowsArgumentOutOfRangeException(Person.Genders newGender)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _Person.ChangeGender(newGender));
            Assert.That(ex.ParamName, Is.EqualTo("gender"));
        }
    }
}