using Projekt.Models;
using ModelsUnitTests.Helpers;
namespace ModelsUnitTests;

public class RoomUnitTests
{
    private Room _Room = null!;
    [Test]
    [TestCase("Room 101", 30)]
    [TestCase("Room 102", 25)]
    [TestCase("Room 103", 20)]
    public void CreateRoom_WithValidInputs_ReturnsRoomWithCorrectAttributes(string name, int size)
    {
        _Room = TestUtils.CreateRoom(name, size);
        Assert.That(_Room.Name, Is.EqualTo(name));
        Assert.That(_Room.Size, Is.EqualTo(size));
    }
    [Test]
    [TestCase("Room 101", 30, "Room 102")]
    [TestCase("Room 102", 25, "Room 103")]
    [TestCase("Room 103", 20, "Room 104")]
    public void ChangeName_WhenSet_ReturnsNewName(string name, int size, string newName)
    {
        _Room = TestUtils.CreateRoom(name, size);
        _Room.ChangeName(newName);
        Assert.That(_Room.Name, Is.EqualTo(newName));
    }
    [Test]
    [TestCase("Room 101", 30, 35)]
    [TestCase("Room 102", 25, 30)]
    [TestCase("Room 103", 20, 25)]
    public void ChangeSize_WhenSet_ReturnsNewSize(string name, int size, int newSize)
    {
        _Room = TestUtils.CreateRoom(name, size);
        _Room.ChangeSize(newSize);
        Assert.That(_Room.Size, Is.EqualTo(newSize));
    }
}
