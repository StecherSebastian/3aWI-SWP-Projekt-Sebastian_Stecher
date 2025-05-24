using static Projekt.Models.Student;

namespace Projekt.Models
{
    public class School
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        private List<Classroom> _Classrooms = new List<Classroom>();
        public List<Classroom> Classrooms { get { return _Classrooms; } set { _Classrooms = value ?? new List<Classroom>(); } }
        private List<Student> _Students = new List<Student>();
        public List<Student> Students { get { return _Students; } set { _Students = value ?? new List<Student>(); } }
        public School(string name)
        {
            Name = name;
        }
        public void AddStudent(Student student)
        {
            _Students.Add(student);
        }
        public void RemoveStudent(Student student)
        {
            _Students.Remove(student);
        }
        public void ClearStudents()
        {
            _Students.Clear();
        }
        public void AddClassroom(Classroom classroom)
        {
            _Classrooms.Add(classroom);
        }
        public void RemoveClassroom(Classroom classroom)
        {
            _Classrooms.Remove(classroom);
        }
        public void ClearClassrooms()
        {
            _Classrooms.Clear();
        }
        public int CountStudents()
        {
            return _Students.Count;
        }
        public int CountStudentsByGender(Person.Genders gender)
        {
            return _Students.Where(x => x.Gender == gender).Count();
        }
        public int CountStudentsBySchoolclass(Student.Schoolclasses schoolclass)
        {
            return _Students.Where(x => x.Schoolclass == schoolclass).Count();
        }
        public int CountStudentsInSchoolclassByGender(Student.Schoolclasses schoolclass, Person.Genders gender)
        {
            return _Students.Where(x => x.Schoolclass == schoolclass && x.Gender == gender).Count();
        }
        public int CountStudentsByTrack(Student.Tracks track)
        {
            return _Students.Where(x => x.Track == track).Count();
        }
        public int CountStudentsInTrackByGender(Student.Tracks track, Person.Genders gender)
        {
            return _Students.Where(x => x.Track == track).Count();
        }
        public double GetAverageAgeOfStudents()
        {
            return _Students.Average(x => x.Age);
        }
        public double GetGenderPercentageInSchoolclass(Person.Genders gender, Student.Schoolclasses schoolclass)
        {
            int numberOfStudents = CountStudentsBySchoolclass(schoolclass);
            return numberOfStudents == 0 ? 0 : CountStudentsInSchoolclassByGender(schoolclass, gender) / numberOfStudents * 100;
        }
        public double GetGenderPercentageInTrack(Person.Genders gender, Student.Tracks track)
        {
            int numberOfStudents = CountStudentsByTrack(track);
            return numberOfStudents == 0 ? 0 : CountStudentsInTrackByGender(track, gender) / numberOfStudents * 100;
        }
        public int CountClassrooms()
        {
            return _Classrooms.Count;
        }
        public List<string> GetClassroomsWithCynap()
        {
            return _Classrooms.Where(x => x.Cynap).Select(x => x.Name).ToList();
        }
        public List<(string, int)> GetClassroomsWithStudentCounts()
        {
            return _Classrooms.Select(x => (x.Name, x.Students.Count)).ToList();
        }
        public bool IsClassroomBigEnough(Classroom classroom, Student.Schoolclasses schoolclass)
        {
            return CountStudentsBySchoolclass(schoolclass) <= classroom.Seats;
        }
    }
}
