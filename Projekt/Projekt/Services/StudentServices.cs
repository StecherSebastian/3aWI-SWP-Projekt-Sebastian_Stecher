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
            Student? student = _Context.Students.FirstOrDefault(s => s.ID == id);
            if (student == null) throw new KeyNotFoundException("Student not found");
            student.ChangeFirstName(request.FirstName);
            student.ChangeLastName(request.LastName);
            student.ChangeBirthdate((DateTime)request.BirthDate);
            student.ChangeGender((Person.Genders)request.Gender);
            student.ChangeSchoolclass((Student.Schoolclasses)request.Schoolclass);
            student.ChangeTrack((Student.Tracks)request.Track);
            _Context.SaveChanges();
            return student;
        }
    }
}
