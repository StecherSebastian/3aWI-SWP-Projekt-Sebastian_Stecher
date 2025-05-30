using Projekt.Models;

namespace ModelsUnitTests;
public class ClassroomUnitTests
{
    private Classroom _Classroom = null!;
    private Student _Student = null!;
    [SetUp]
    public void Setup()
    {
        _Classroom = new Classroom("Room 101", 30, 15, false);
        _Student = new Student("Sebastian", "Stecher", new DateTime(2008, 5, 10), 0, 0, 0);
    }

    [Test]
    [TestCase("Room 101", 30, 15, false)]
    [TestCase("Room 102", 25, 10, true)]
    [TestCase("Room 103", 99, 0, false)]
    public void CreateClassroom_WithValidValues_ReturnsClassroomWithCorrectAttributes(string name, int size, int seats, bool cynap)
    {
        _Classroom = new Classroom(name, size, seats, cynap);
        Assert.That(_Classroom.Name, Is.EqualTo(name));
        Assert.That(_Classroom.Size, Is.EqualTo(size));
        Assert.That(_Classroom.Seats, Is.EqualTo(seats));
        Assert.That(_Classroom.Cynap, Is.EqualTo(cynap));
    }
    [Test]
    [TestCase("Room 101", 30, -12, false)]
    public void CreateClassroom_WithInvalidSeats_ThrowsArgumentOutOfRangeException(string name, int size, int seats, bool cynap)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Classroom(name, size, seats, cynap));
        Assert.That(ex.ParamName, Is.EqualTo("seats"));
    }
    [Test]
    [TestCase(10)]
    [TestCase(15)]
    public void ChangeSeatsCount_WhenSet_ReturnsNewNumberOfSeats(int newSeats)
    {
        _Classroom.ChangeSeatsCount(newSeats);
        Assert.That(_Classroom.Seats, Is.EqualTo(newSeats));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(100)]
    public void ChangeSeatsCount_SetInvalidValue_ThrowsArgumentOutOfRangeException(int newSeats)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _Classroom.ChangeSeatsCount(newSeats));
        Assert.That(ex.ParamName, Is.EqualTo("seats"));
    }
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void ChangeCynap_WhenSet_ReturnsNewValue(bool newCynap)
    {
        _Classroom.ChangeCynap(newCynap);
        Assert.That(_Classroom.Cynap, Is.EqualTo(newCynap));
    }
    [Test]
    [TestCase("Room 102")]
    public void ChangeName_WhenSet_ReturnsNewName(string newName)
    {
        _Classroom.ChangeName(newName);
        Assert.That(_Classroom.Name, Is.EqualTo(newName));
    }
    [Test]
    public void AddStudent_WhenStudentIsValid_AddsStudentToClassroom()
    {
        _Classroom.AddStudent(_Student);
        Assert.That(_Classroom.Students, Does.Contain(_Student));
    }
    [Test]
    public void AddStudent_WhenStudentIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _Classroom.AddStudent(null!));
        Assert.That(ex.ParamName, Is.EqualTo("student"));
    }
    [Test]
    public void RemoveStudent_WhenStudentExists_RemovesStudentFromClassroom()
    {
        _Classroom.AddStudent(_Student);
        Assert.That(_Classroom.Students, Does.Contain(_Student));
        _Classroom.RemoveStudent(_Student);
        Assert.That(_Classroom.Students, Does.Not.Contain(_Student));
    }
    [Test]
    public void RemoveStudent_WhenStudentIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _Classroom.RemoveStudent(null!));
        Assert.That(ex.ParamName, Is.EqualTo("student"));
    }
    [Test]
    public void RemoveStudent_WhenStudentDoesNotExist_ThrowsInvalidOperationException()
    {
        var ex = Assert.Throws<InvalidOperationException>(() => _Classroom.RemoveStudent(_Student));
        Assert.That(ex.Message, Does.Contain("Student not found in classroom"));
    }
    [Test]
    public void ClearStudents_ClearsAllStudents()
    {
        _Classroom.AddStudent(_Student);
        Assert.That(_Classroom.Students, Is.Not.Empty);
        _Classroom.ClearStudents();
        Assert.That(_Classroom.Students, Is.Empty);
    }
}
