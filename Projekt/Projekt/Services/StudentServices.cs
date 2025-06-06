using Projekt.Utilities;
using Projekt.Database;
using Projekt.Models;

namespace Projekt.Services
{
    public class StudentServices
    {
        private readonly ProjektDbContext _Context;
        public StudentServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public Student CreateStudent(Student request)
        {
            Validator.ValidateString(request.FirstName, "FirstName");
            Validator.ValidateString(request.LastName, "LastName");
            Validator.ValidateEnumValue(request.Gender, "Gender");
            Validator.ValidateEnumValue(request.Schoolclass, "Schoolclass");
            Validator.ValidateEnumValue(request.Track, "Track");
            Student student = new Student(request.FirstName, request.LastName, request.BirthDate, request.Gender, request.Schoolclass, request.Track);
            _Context.Students.Add(student);
            _Context.SaveChanges();
            return student;
        }
        public Student GetStudentByID(int id)
        {
            Student? student = _Context.Students.FirstOrDefault(c => c.ID == id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            return student;
        }
        public List<Student> GetAllStudents() =>
             _Context.Students.ToList();
        public void DeleteStudent(int id)
        {
            Student? student = _Context.Students.FirstOrDefault(c => c.ID == id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            _Context.Students.Remove(student);
            _Context.SaveChanges();
        }
        public void DeleteStudents(List<int> studentIDs)
        {
            List<Student> students = _Context.Students.Where(s => studentIDs.Contains(s.ID)).ToList();
            _Context.Students.RemoveRange(students);
            _Context.SaveChanges();
        }
        public Student UpdateStudent(int id, Student request)
        {
            Validator.ValidateString(request.FirstName, "FirstName");
            Validator.ValidateString(request.LastName, "LastName");
            Validator.ValidateEnumValue(request.Gender, "Gender");
            Validator.ValidateEnumValue(request.Schoolclass, "Schoolclass");
            Validator.ValidateEnumValue(request.Track, "Track");
            Validator.ValidateEnumValue(request.Schoolclass, nameof(request.Schoolclass));
            Student? student = _Context.Students.FirstOrDefault(s => s.ID == id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            student.ChangeFirstName(request.FirstName);
            student.ChangeLastName(request.LastName);
            student.ChangeBirthdate(request.BirthDate);
            student.ChangeGender(request.Gender);
            student.ChangeSchoolclass(request.Schoolclass);
            student.ChangeTrack(request.Track);
            _Context.SaveChanges();
            return student;
        }
    }
}
