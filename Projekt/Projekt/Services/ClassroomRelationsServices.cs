using Microsoft.EntityFrameworkCore;
using Projekt.Database;
using Projekt.Models;

namespace Projekt.Services
{
    public class ClassroomRelationsServices
    {
        private readonly ProjektDbContext _Context;
        public ClassroomRelationsServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public void AddStudentToClassroom(int classroomID, int studentID)
        {
            Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == classroomID);
            if (classroom == null)
            {
                throw new KeyNotFoundException("Classroom not found");
            }
            Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentID);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }
            classroom.AddStudent(student);
            _Context.SaveChanges();
        }
        public void RemoveStudentFromClassroom(int classroomID, int studentID)
        {
            Classroom? classroom = _Context.Classrooms.Include(s => s.Students).FirstOrDefault(s => s.ID == classroomID);
            if (classroom == null)
            {
                throw new KeyNotFoundException("Classroom not found");
            }
            Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentID);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }
            classroom.RemoveStudent(student);
            _Context.SaveChanges();
        }
        public void RemoveStudentsFromClassroom(int classroomID, List<int> studentIDs)
        {
            Classroom? classroom = _Context.Classrooms.Include(s => s.Students).FirstOrDefault(s => s.ID == classroomID);
            if (classroom == null)
            {
                throw new KeyNotFoundException("Classroom not found");
            }
            List<Student> students = _Context.Students
                .Where(s => studentIDs.Contains(s.ID) && classroom.Students.Contains(s))
                .ToList();
            if (students.Count == 0) throw new KeyNotFoundException("No students found to remove from classroom");
            foreach (Student student in students)
            {
                classroom.RemoveStudent(student);
            }
            _Context.SaveChanges();
        }
        public void RemoveAllStudentsFromClassroom(int classroomID)
        {
            Classroom? classroom = _Context.Classrooms.Include(s => s.Students).FirstOrDefault(s => s.ID == classroomID);
            if (classroom == null)
            {
                throw new KeyNotFoundException("Classroom not found");
            }
            classroom.ClearStudents();
            _Context.SaveChanges();
        }
    }
}
