using ModelsUnitTests.Helpers;
using Projekt.Models;
namespace ModelsUnitTests;

public class StudentUnitTests
{
    private Student _Student = null!;
    [Test]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 2, 0)]
    [TestCase("Sebastian", "Stecher", "10.05.2008", 0, 3, 0)]
    [TestCase("Florian", "Fleisch", "21.11.2007", 0, 4, 2)]
    [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, 5, 7)]
    public void CreateStudent_WithValidInputs_ReturnsStudentWithCorrectAttributes(string firstName, string lastName, string birthdate, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track)
    {
        _Student = TestUtils.CreateStudent(firstName, lastName, birthdate, gender, schoolclass, track);
        Assert.That(_Student.FirstName, Is.EqualTo(firstName));
        Assert.That(_Student.LastName, Is.EqualTo(lastName));
        Assert.That(_Student.Gender, Is.EqualTo(gender));
        Assert.That(_Student.Schoolclass, Is.EqualTo(schoolclass));
        Assert.That(_Student.Track, Is.EqualTo(track));
    }
    [Test]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 2, 0, 0)]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 2, 0, 3)]
    [TestCase("Sebastian", "Stecher", "10.05.2008", 0, 3, 0, 4)]
    [TestCase("Florian", "Fleisch", "21.11.2007", 0, 4, 2, 2)]
    [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, 5, 7, 1)]
    public void ChangeSchoolclass_WhenSet_ReturnsNewSchoolclass(string firstName, string lastName, string birthdate, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track, Student.Schoolclasses newSchoolclass)
    {
        _Student = TestUtils.CreateStudent(firstName, lastName, birthdate, gender, schoolclass, track);
        _Student.ChangeSchoolclass(newSchoolclass);
        Assert.That(_Student.Schoolclass, Is.EqualTo(newSchoolclass));
    }
    [Test]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 2, 0, 0)]
    [TestCase("Anna", "Müller", "15.03.2007", 1, 2, 0, 3)]
    [TestCase("Sebastian", "Stecher", "10.05.2008", 0, 3, 0, 4)]
    [TestCase("Florian", "Fleisch", "21.11.2007", 0, 4, 2, 2)]
    [TestCase("Leonie-Sophie", "Stecher", "10.05.2008", 0, 5, 7, 1)]
    public void ChangeTrack_WhenSet_ReturnsNewTrack(string firstName, string lastName, string birthdate, Person.Genders gender, Student.Schoolclasses schoolclass, Student.Tracks track, Student.Tracks newTrack)
    {
        _Student = TestUtils.CreateStudent(firstName, lastName, birthdate, gender, schoolclass, track);
        _Student.ChangeTrack(newTrack);
        Assert.That(_Student.Track, Is.EqualTo(newTrack));
    }
}
