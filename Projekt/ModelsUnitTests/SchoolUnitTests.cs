using Projekt.Models;
using ModelsUnitTests.Helpers;

namespace ModelsUnitTests;

public class SchoolUnitTests
{
    private School _School = null!;
    private Classroom _Classroom1 = null!;
    private Classroom _Classroom2 = null!;
    private Student _Student1 = null!;
    private Student _Student2 = null!;
    private Student _Student3 = null!;
    [SetUp]
    public void Setup()
    {
        _School = new School("HTL Dornbirn");
        _Classroom1 = new Classroom("Room 101", 30, 15, false);
        _Classroom2 = new Classroom("Room 102", 25, 10, true);
        _Student1 = new Student("Sebastian", "Stecher", new DateTime(2008, 5, 10), 0, 0, 0);
        _Student2 = new Student("Firstname", "Lastname", new DateTime(2007, 8, 12), Person.Genders.w, 0, 0);
        _Student3 = new Student("Firstname", "Lastname", new DateTime(2009, 12, 23), Person.Genders.d, 0, 0);
    }

    [Test]
    [TestCase("HTL Dornbirn")]
    public void CreateSchool_WithValidValues_ReturnsSchoolWithCorrectAttributes(string name)
    {
        _School = new School(name);
        Assert.That(_School.Name, Is.EqualTo(name));
    }
    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    public void CreateSchool_WithInvalidName_ThrowsArgumentException(string? name)
    {
        var ex = Assert.Throws<ArgumentException>(() => new School(name));
        Assert.That(ex.ParamName, Is.EqualTo("name"));
    }
    [Test]
    [TestCase("HTL Bregenz")]
    public void ChangeName_WhenSet_ReturnsNewName(string newName)
    {
        _School.ChangeName(newName);
        Assert.That(_School.Name, Is.EqualTo(newName));
    }
    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    public void ChangeName_SetInvalidValue_ThrowsArgumentException(string? newName)
    {
        var ex = Assert.Throws<ArgumentException>(() => _School.ChangeName(newName));
        Assert.That(ex.ParamName, Is.EqualTo("name"));
    }
    [Test]
    public void AddStudent_WhenStudentIsValid_AddsStudentToSchool()
    {
        _School.AddStudent(_Student1);
        Assert.That(_School.Students, Does.Contain(_Student1));
    }
    [Test]
    public void AddStudent_WhenStudentIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _School.AddStudent(null!));
        Assert.That(ex.ParamName, Is.EqualTo("student"));
    }
    [Test]
    public void RemoveStudent_WhenStudentExists_RemovesStudentFromSchool()
    {
        _School.AddStudent(_Student1);
        _School.RemoveStudent(_Student1);
        Assert.That(_School.Students, Does.Not.Contain(_Student1));
    }
    [Test]
    public void RemoveStudent_WhenStudentIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _School.RemoveStudent(null!));
        Assert.That(ex.ParamName, Is.EqualTo("student"));
    }
    [Test]
    public void RemoveStudent_WhenStudentDoesNotExist_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _School.RemoveStudent(_Student1));
    }
    [Test]
    public void ClearStudents_RemovesAllStudentsFromSchool()
    {
        _School.AddStudent(_Student1);
        Assert.That(_School.Students, Is.Not.Empty);
        _School.ClearStudents();
        Assert.That(_School.Students, Is.Empty);
    }
    [Test]
    public void AddClassroom_WhenClassroomIsValid_AddsClassroomToSchool()
    {
        _School.AddClassroom(_Classroom1);
        Assert.That(_School.Classrooms, Does.Contain(_Classroom1));
    }
    [Test]
    public void AddClassroom_WhenClassroomIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _School.AddClassroom(null!));
        Assert.That(ex.ParamName, Is.EqualTo("classroom"));
    }
    [Test]
    public void RemoveClassroom_WhenClassroomExists_RemovesClassroomFromSchool()
    {
        _School.AddClassroom(_Classroom1);
        _School.RemoveClassroom(_Classroom1);
        Assert.That(_School.Classrooms, Does.Not.Contain(_Classroom1));
    }
    [Test]
    public void RemoveClassroom_WhenClassroomIsNull_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _School.RemoveClassroom(null!));
        Assert.That(ex.ParamName, Is.EqualTo("classroom"));
    }
    [Test]
    public void RemoveClassroom_WhenClassroomDoesNotExist_ThrowsInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _School.RemoveClassroom(_Classroom1));
    }
    [Test]
    public void ClearClassrooms_RemovesAllClassroomsFromSchool()
    {
        _School.AddClassroom(_Classroom1);
        Assert.That(_School.Classrooms, Is.Not.Empty);
        _School.ClearClassrooms();
        Assert.That(_School.Classrooms, Is.Empty);
    }
    [Test]
    public void CountStudents_ReturnsCorrectStudentCount()
    {
        Assert.That(_School.CountStudents(), Is.EqualTo(0));
        _School.AddStudent(_Student1);
        Assert.That(_School.CountStudents(), Is.EqualTo(1));
        _School.AddStudent(_Student1);
        Assert.That(_School.CountStudents(), Is.EqualTo(2));
        _School.ClearStudents();
        Assert.That(_School.CountStudents(), Is.EqualTo(0));
    }
    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    public void CountStudentsByGender_ValidGender_ReturnsCorrectCountByGender(Person.Genders gender, int count)
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.CountStudentsByGender(gender), Is.EqualTo(count));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsByGender_InvalidGender_ThrowsArgumentOutOfRangeException(Person.Genders gender)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsByGender(gender));
        Assert.That(ex.ParamName, Is.EqualTo("gender"));
    }
    [Test]
    [TestCase(0, 3)]
    public void CountStudentsBySchoolclass_ValidSchoolclass_ReturnsCorrectCountBySchoolclass(Student.Schoolclasses schoolclass, int count)
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.CountStudentsBySchoolclass(schoolclass), Is.EqualTo(count));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsBySchoolclass_InvalidSchoolclass_ThrowsArgumentOutOfRangeException(Student.Schoolclasses schoolclass)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsBySchoolclass(schoolclass));
        Assert.That(ex.ParamName, Is.EqualTo("schoolclass"));
    }
    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    public void CountStudentsInSchoolclassByGender_ValidValues_ReturnsCorrectCount(Person.Genders gender, int count)
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.CountStudentsInSchoolclassByGender(0, gender), Is.EqualTo(count));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsInSchoolclassByGender_InvalidSchool_ThrowsArgumentOutOfRangeException(Student.Schoolclasses schoolclass)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsInSchoolclassByGender(schoolclass, 0));
        Assert.That(ex.ParamName, Is.EqualTo("schoolclass"));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsInSchoolclassByGender_InvalidGender_ThrowsArgumentOutOfRangeException(Person.Genders gender)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsInSchoolclassByGender(0, gender));
        Assert.That(ex.ParamName, Is.EqualTo("gender"));
    }
    [Test]
    [TestCase(0, 3)]
    public void CountStudentsByTrack_ReturnsCorrectCountByTrack(Student.Tracks track, int count)
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.CountStudentsByTrack(track), Is.EqualTo(count));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsByTrack_InvalidTrack_ThrowsArgumentOutOfRangeException(Student.Tracks track)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsByTrack(track));
        Assert.That(ex.ParamName, Is.EqualTo("track"));
    }
    [Test]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    public void CountStudentsInTrackByGender_ValidValues_ReturnsCorrectCount(Person.Genders gender, int count)
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.CountStudentsInTrackByGender(0, gender), Is.EqualTo(count));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsInTrackByGender_InvalidTrack_ThrowsArgumentOutOfRangeException(Student.Tracks track)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsInTrackByGender(track, 0));
        Assert.That(ex.ParamName, Is.EqualTo("track"));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void CountStudentsInTrackByGender_InvalidGender_ThrowsArgumentOutOfRangeException(Person.Genders gender)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.CountStudentsInSchoolclassByGender(0, gender));
        Assert.That(ex.ParamName, Is.EqualTo("gender"));
    }
    [Test]
    [TestCase("10.05.2008", "12.08.2007", "23.12.2007")]
    [TestCase("01.01.2000", "05.05.1995", "31.12.2010")]
    public void AverageAgeOfStudents_ReturnsCorrectAverageAge(string birthdateString1, string birthdateString2, string birthdateString3)
    {
        DateTime birthdate1 = TestUtils.ParseBirthdate(birthdateString1);
        DateTime birthdate2 = TestUtils.ParseBirthdate(birthdateString2);
        DateTime birthdate3 = TestUtils.ParseBirthdate(birthdateString3);
        int age1 = TestUtils.CalculateAge(birthdate1);
        int age2 = TestUtils.CalculateAge(birthdate2);
        int age3 = TestUtils.CalculateAge(birthdate3);
        _Student1.ChangeBirthdate(birthdate1);
        _Student2.ChangeBirthdate(birthdate2);
        _Student3.ChangeBirthdate(birthdate3);
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        double expectedAverageAge = (age1 + age2 + age3) / 3.0;
        Assert.That(_School.GetAverageAgeOfStudents(), Is.EqualTo(expectedAverageAge));
    }
    [Test]
    [TestCase(0, 0, 33.33)]
    [TestCase(1, 0, 33.33)]
    [TestCase(2, 0, 33.33)]
    [TestCase(0, 1, 0)]
    public void GetGenderPercentageInSchoolclass_ReturnsCorrectGenderPercentageInSchoolclass(Person.Genders gender, Student.Schoolclasses schoolclass, double percentage) 
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.GetGenderPercentageInSchoolclass(gender, schoolclass), Is.EqualTo(percentage));
    }
    [Test]
    [TestCase(-1, 0, "gender")]
    [TestCase(99, 0, "gender")]
    [TestCase(0, -1, "schoolclass")]
    [TestCase(0, 99, "schoolclass")]
    public void GetGenderPercentageInSchoolclass_InvalidInputs_ThrowsArgumentOutOfRangeException(Person.Genders gender, Student.Schoolclasses schoolclass, string paramName)
    {
        _School.AddStudent(_Student1);
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.GetGenderPercentageInSchoolclass(gender, schoolclass));
        Assert.That(ex.ParamName, Is.EqualTo(paramName));
    }
    [Test]
    [TestCase(0, 0, 33.33)]
    [TestCase(1, 0, 33.33)]
    [TestCase(2, 0, 33.33)]
    [TestCase(0, 1, 0)]
    public void GetGenderPercentageInTrack_ReturnsCorrectGenderPercentageTrack(Person.Genders gender, Student.Tracks track, double percentage)
    {
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        _School.AddStudent(_Student3);
        Assert.That(_School.GetGenderPercentageInTrack(gender, track), Is.EqualTo(percentage));
    }
    [Test]
    [TestCase(-1, 0, "gender")]
    [TestCase(99, 0, "gender")]
    [TestCase(0, -1, "track")]
    [TestCase(0, 99, "track")]
    public void GetGenderPercentageInTrack_InvalidInputs_ThrowsArgumentOutOfRangeException(Person.Genders gender, Student.Tracks track, string paramName)
    {
        _School.AddStudent(_Student1);
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.GetGenderPercentageInTrack(gender, track));
        Assert.That(ex.ParamName, Is.EqualTo(paramName));
    }
    [Test]
    public void CountClassrooms_ReturnsCorrectCountClassrooms() {
        Assert.That(_School.CountClassrooms(), Is.EqualTo(0));
        _School.AddClassroom(_Classroom1);
        Assert.That(_School.CountClassrooms(), Is.EqualTo(1));
        _School.AddClassroom(_Classroom1);
        Assert.That(_School.CountClassrooms(), Is.EqualTo(2));
        _School.ClearClassrooms();
        Assert.That(_School.CountClassrooms(), Is.EqualTo(0));
    }
    [Test]
    public void GetClassroomsWithCynap_ReturnsListWithCorrectClassroomNames()
    {
        _School.AddClassroom(_Classroom1);
        _Classroom1.ChangeCynap(true);
        var classroomsWithCynap = _School.GetClassroomsWithCynap();
        Assert.That(classroomsWithCynap, Does.Contain(_Classroom1.Name));
    }
    [Test]
    public void GetClassroomsWithCynap_NoClassroomsWithCynap_ReturnsEmptyList()
    {
        _School.AddClassroom(_Classroom1);
        _Classroom1.ChangeCynap(false);
        var classroomsWithCynap = _School.GetClassroomsWithCynap();
        Assert.That(classroomsWithCynap, Is.Empty);
    }
    [Test]
    public void GetClassroomWithStudentCount_ReturnsCorrectClassroomWithStudentCount()
    {
        _School.AddClassroom(_Classroom1);
        _School.AddClassroom(_Classroom2);
        _Classroom1.AddStudent(_Student1);
        _Classroom1.AddStudent(_Student2);
        var classroomWithCount = _School.GetClassroomsWithStudentCount();
        Assert.That(classroomWithCount, Does.Contain((_Classroom1.Name, 2)));
        Assert.That(classroomWithCount, Does.Contain((_Classroom2.Name, 0)));
    }
    [Test]
    public void GetClassroomWithStudentCount_NoClassrooms_ReturnsEmptyList()
    {
        var classroomWithCount = _School.GetClassroomsWithStudentCount();
        Assert.That(classroomWithCount, Is.Empty);
    }
    [Test]
    [TestCase(5, 0, true)]
    [TestCase(1, 0, false)]
    public void IsClassroomBigEnough_ValidInputs_ReturnsCorrectResult(int seats, Student.Schoolclasses schoolclass, bool expected)
    {
        _School.AddClassroom(_Classroom1);
        _Classroom1.ChangeSeatsCount(seats);
        _School.AddStudent(_Student1);
        _School.AddStudent(_Student2);
        Assert.That(_School.IsClassroomBigEnough(_Classroom1, schoolclass), Is.EqualTo(expected));
    }
    [Test]
    [TestCase(-1)]
    [TestCase(99)]
    public void IsClassroomBigEnough_InvalidSchoolclass_ThrowsArgumentOutOfRangeException(Student.Schoolclasses schoolclass)
    {
        _School.AddClassroom(_Classroom1);
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _School.IsClassroomBigEnough(_Classroom1, schoolclass));
        Assert.That(ex.ParamName, Is.EqualTo("schoolclass"));
    }
    [Test]
    public void IsClassroomBigEnough_NullClassroom_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _School.IsClassroomBigEnough(null!, 0));
        Assert.That(ex.ParamName, Is.EqualTo("classroom"));
    }
}