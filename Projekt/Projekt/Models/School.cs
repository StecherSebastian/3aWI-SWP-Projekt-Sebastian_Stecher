using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class School
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [Length(2, 50, ErrorMessage = "Name has to be at least 3 Characters long up to a maximum of 50.")]
        public string Name { get; private set; } = null!;
        public List<Classroom> Classrooms { get; private set; } = new List<Classroom>();
        public List<Student> Students { get; private set; } = new List<Student>();
        public School(string name)
        {
            Name = name;
        }
        protected School() {}
        public void ChangeName(string name)
        {
            Utilities.Validator.ValidateString(name, nameof(name));
            Name = name;
        }
        public void AddStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            Students.Add(student);
        }
        public void RemoveStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            if (!Students.Contains(student))
            {
                throw new InvalidOperationException("Student not found in school.");
            }
            Students.Remove(student);
        }
        public void ClearStudents()
        {
            Students.Clear();
        }
        public void AddClassroom(Classroom classroom)
        {
            ArgumentNullException.ThrowIfNull(classroom);
            Classrooms.Add(classroom);
        }
        public void RemoveClassroom(Classroom classroom)
        {
            ArgumentNullException.ThrowIfNull(classroom);
            if (!Classrooms.Contains(classroom))
            {
                throw new InvalidOperationException("Classroom not found in school.");
            }
            Classrooms.Remove(classroom);
        }
        public void ClearClassrooms()
        {
            Classrooms.Clear();
        }
        public int CountStudents()
        {
            return Students.Count;
        }
        public int CountStudentsByGender(Person.Genders gender)
        {
            Utilities.Validator.ValidateEnumValue(gender, nameof(gender));
            return Students.Where(x => x.Gender == gender).Count();
        }
        public int CountStudentsBySchoolclass(Student.Schoolclasses schoolclass)
        {
            Utilities.Validator.ValidateEnumValue(schoolclass, nameof(schoolclass));
            return Students.Where(x => x.Schoolclass == schoolclass).Count();
        }
        public int CountStudentsInSchoolclassByGender(Student.Schoolclasses schoolclass, Person.Genders gender)
        {
            Utilities.Validator.ValidateEnumValue(schoolclass, nameof(schoolclass));
            Utilities.Validator.ValidateEnumValue(gender, nameof(gender));
            return Students.Where(x => x.Schoolclass == schoolclass && x.Gender == gender).Count();
        }
        public int CountStudentsByTrack(Student.Tracks track)
        {
            Utilities.Validator.ValidateEnumValue(track, nameof(track));
            return Students.Where(x => x.Track == track).Count();
        }
        public int CountStudentsInTrackByGender(Student.Tracks track, Person.Genders gender)
        {
            Utilities.Validator.ValidateEnumValue(track, nameof(track));
            Utilities.Validator.ValidateEnumValue(gender, nameof(gender));
            return Students.Where(x => x.Track == track && x.Gender == gender).Count();
        }
        public double GetAverageAgeOfStudents()
        {
            if (Students.Count == 0)
            {
                return 0;
            }
            return Students.Average(x => x.Age);
        }
        public double GetGenderPercentageInSchoolclass(Person.Genders gender, Student.Schoolclasses schoolclass)
        {
            Utilities.Validator.ValidateEnumValue(gender, nameof(gender));
            Utilities.Validator.ValidateEnumValue(schoolclass, nameof(schoolclass));
            int numberOfStudents = CountStudentsBySchoolclass(schoolclass);
            return numberOfStudents == 0 ? 0 : Math.Round((double)CountStudentsInSchoolclassByGender(schoolclass, gender) / numberOfStudents * 100, 2);
        }
        public double GetGenderPercentageInTrack(Person.Genders gender, Student.Tracks track)
        {
            Utilities.Validator.ValidateEnumValue(gender, nameof(gender));
            Utilities.Validator.ValidateEnumValue(track, nameof(track));
            int numberOfStudents = CountStudentsByTrack(track);
            return numberOfStudents == 0 ? 0 : Math.Round((double)CountStudentsInTrackByGender(track, gender) / numberOfStudents * 100, 2);
        }
        public int CountClassrooms()
        {
            return Classrooms.Count;
        }
        public List<string> GetClassroomsWithCynap()
        {
            return Classrooms.Where(x => x.Cynap).Select(x => x.Name).ToList();
        }
        public List<(string, int)> GetClassroomsWithStudentCount()
        {
            return Classrooms.Select(x => (x.Name, x.Students.Count)).ToList();
        }
        public bool IsClassroomBigEnough(Classroom classroom, Student.Schoolclasses schoolclass)
        {
            ArgumentNullException.ThrowIfNull(classroom);
            Utilities.Validator.ValidateEnumValue(schoolclass, nameof(schoolclass));
            return CountStudentsBySchoolclass(schoolclass) <= classroom.Seats;
        }
    }
}
