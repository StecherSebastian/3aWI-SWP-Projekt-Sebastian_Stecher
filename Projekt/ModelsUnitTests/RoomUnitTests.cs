using Projekt.Models;
using ModelsUnitTests.Helpers;
namespace ModelsUnitTests;

public class RoomUnitTests
{
    private Room _Room = null!;
    [SetUp]
    public void Setup()
    {
        _Room = new Room("Room 101", 30);
    }
    [Test]
    [TestCase("Room 101", 30)]
    [TestCase("Room 102", 25)]
    [TestCase("Room 103", 20)]
    public void CreateRoom_WithValidValues_ReturnsRoomWithCorrectAttributes(string name, int size)
    {
        _Room = TestUtils.CreateRoom(name, size);
        Assert.That(_Room.Name, Is.EqualTo(name));
        Assert.That(_Room.Size, Is.EqualTo(size));
    }
    [Test]
    [TestCase(null, 30)]
    [TestCase(" ", 25)]
    public void CreateRoom_WithInvalidName_ThrowsArgumentException(string? name, int size)
    {
        var ex = Assert.Throws<ArgumentException>(() => TestUtils.CreateRoom(name, size));
        Assert.That(ex.ParamName, Is.EqualTo("name"));
    }
    [Test]
    [TestCase("Room 101", -1)]
    public void CreateRoom_WithInvalidSize_ThrowsArgumentOutOfRangeException(string name, int size)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => TestUtils.CreateRoom(name, size));
        Assert.That(ex.ParamName, Is.EqualTo("size"));
    }
    [Test]
    [TestCase("Room 102")]
    public void ChangeName_WhenSet_ReturnsNewName(string newName)
    {
        _Room.ChangeName(newName);
        Assert.That(_Room.Name, Is.EqualTo(newName));
    }
    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    public void ChangeName_SetInvalidValue_ThrowsArgumentException(string? newName)
    {
        var ex = Assert.Throws<ArgumentException>(() => _Room.ChangeName(newName));
        Assert.That(ex.ParamName, Is.EqualTo("name"));
    }
    [Test]
    [TestCase(35)]
    [TestCase(52)]
    public void ChangeSize_WhenSet_ReturnsNewSize(int newSize)
    {
        _Room.ChangeSize(newSize);
        Assert.That(_Room.Size, Is.EqualTo(newSize));
    }
    [Test]
    [TestCase(-1)]
    public void ChangeSize_SetInvalidValue_ThrowsArgumentOutOfRangeException(int newSize)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _Room.ChangeSize(newSize));
        Assert.That(ex.ParamName, Is.EqualTo("size"));
    }
}
