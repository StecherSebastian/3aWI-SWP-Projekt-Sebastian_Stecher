using ModelsUnitTests.Helpers;
using Projekt.Models;

namespace ModelsUnitTests;
public class StudentUnitTests
{
    private Student _Student = null!;
    [SetUp]
    public void Setup()
    {
        _Student = new Student("Sebastian", "Stecher", new DateTime(2008, 5, 10), 0, 0, 0);
    }
    [Test]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 2, 0)]
    [TestCase("Sebastian", "Stecher", "10.05.2008", 0, 3, 0)]
    [TestCase("Florian", "Fleisch", "21.11.2007", 0, 4, 2)]
    [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, 5, 7)]
    public void CreateStudent_WithValidValues_ReturnsStudentWithCorrectAttributes(string firstName, string lastName, string birthdate, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track)
    {
        _Student = TestUtils.CreateStudent(firstName, lastName, birthdate, gender, schoolclass, track);
        Assert.That(_Student.FirstName, Is.EqualTo(firstName));
        Assert.That(_Student.LastName, Is.EqualTo(lastName));
        Assert.That(_Student.Gender, Is.EqualTo(gender));
        Assert.That(_Student.Schoolclass, Is.EqualTo(schoolclass));
        Assert.That(_Student.Track, Is.EqualTo(track));
    }
    [Test]
    [TestCase("Anna", "Müller", "15.03.2007", 1, -1, 0)]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 132, 0)]
    public void CreateStudent_WithInvalidSchoolclass_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdate, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreateStudent(firstName, lastName, birthdate, gender, schoolclass, track));
        Assert.That(ex.ParamName, Is.EqualTo("schoolclass"));
    }
    [Test]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 0, -2)]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 0, 99)]
    public void CreateStudent_WithInvalidTrack_ThrowsArgumentOutOfRangeException(string firstName, string lastName, string birthdate, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreateStudent(firstName, lastName, birthdate, gender, schoolclass, track));
        Assert.That(ex.ParamName, Is.EqualTo("track"));
    }
    [Test]
    [TestCase(0)]
    public void ChangeSchoolclass_WhenSet_ReturnsNewSchoolclass(Student.Schoolclasses newSchoolclass)
    {
        _Student.ChangeSchoolclass(newSchoolclass);
        Assert.That(_Student.Schoolclass, Is.EqualTo(newSchoolclass));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(233)]
    public void ChangeSchoolclass_SetInvalidValue_ThrowsArgumentOutOfRangeException(Student.Schoolclasses newSchoolclass)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _Student.ChangeSchoolclass(newSchoolclass));
        Assert.That(ex.ParamName, Is.EqualTo("schoolclass"));
    }
    [Test]
    [TestCase(0)]
    public void ChangeTrack_WhenSet_ReturnsNewTrack(Student.Tracks newTrack)
    {
        _Student.ChangeTrack(newTrack);
        Assert.That(_Student.Track, Is.EqualTo(newTrack));
    }
    [Test]
    [TestCase(-2)]
    [TestCase(99)]
    public void ChangeTrack_SetInvalidValue_ThrowsArgumentOutOfRangeException(Student.Tracks newTrack)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _Student.ChangeTrack(newTrack));
        Assert.That(ex.ParamName, Is.EqualTo("track"));
    }
}
