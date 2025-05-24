using Projekt.Models;

namespace ModelsUnitTests;

public class ClassroomUnitTests
{
    Classroom _Classroom = new Classroom("Test", 100, 20, true);
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [TestCase("Room 101", 30, 15, false)]
    [TestCase("Room 102", 25, 10, true)]
    [TestCase("Room 103", 20, 5, false)]
    public void CreateClassroom_WithValidInputs_ReturnsClassroomWithCorrectAttributes(string name, int size, int seats, bool cynap)
    {
        _Classroom = new Classroom(name, size, seats, cynap);
        Assert.That(_Classroom.Name, Is.EqualTo(name));
        Assert.That(_Classroom.Size, Is.EqualTo(size));
        Assert.That(_Classroom.Seats, Is.EqualTo(seats));
        Assert.That(_Classroom.Cynap, Is.EqualTo(cynap));
    }
    [Test]
    [TestCase("Room 101", 30, 15, false, 10)]
    [TestCase("Room 103", 20, 5, false, 15)]
    public void ChangeNumberOfSeats_WhenSet_ReturnsNewNumberOfSeats(string name, int size, int seats, bool cynap, int newSeats)
    {
        _Classroom = new Classroom(name, size, seats, cynap);
        _Classroom.ChangeNumberOfSeats(newSeats);
        Assert.That(_Classroom.Seats, Is.EqualTo(newSeats));
    }
    [Test]
    [TestCase("Room 101", 30, 15, false, true)]
    [TestCase("Room 102", 25, 10, true, false)]
    [TestCase("Room 103", 20, 5, false, true)]
    public void ChangeCynap_WhenSet_ReturnsNewCynap(string name, int size, int seats, bool cynap, bool newCynap)
    {
        _Classroom = new Classroom(name, size, seats, cynap);
        _Classroom.ChangeCynap(newCynap);
        Assert.That(_Classroom.Cynap, Is.EqualTo(newCynap));
    }
    [Test]
    public void AddStudent_WhenStudentIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _Classroom.AddStudent(null!));
        Assert.That(ex.ParamName, Is.EqualTo("student"));
    }
}
