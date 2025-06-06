using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Database;

namespace Projekt.Services
{
    public class SchoolRelationsServices
    {
        private readonly ProjektDbContext _Context;
        public SchoolRelationsServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public void AddClassroomToSchool(int schoolId, int classroomId)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolId);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            Classroom? classroom = _Context.Classrooms.FirstOrDefault(s => s.ID == classroomId);
            if (classroom == null)
            {
                throw new KeyNotFoundException("Classroom not found.");
            }
            school.AddClassroom(classroom);
            _Context.SaveChanges();
        }
        public void RemoveClassroomFromSchool(int schoolID, int classroomID)
        {
            School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            Classroom? classroom = _Context.Classrooms.FirstOrDefault(s => s.ID == classroomID);
            if (classroom == null)
            {
                throw new KeyNotFoundException("Classroom not found.");
            }
            school.RemoveClassroom(classroom);
            _Context.SaveChanges();
        }
        public void RemoveClassroomsFromSchool(int schoolID, List<int> classroomIDs)
        {
            School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            List<Classroom> classrooms = _Context.Classrooms
                .Where(c => classroomIDs.Contains(c.ID) && school.Classrooms.Contains(c))
                .ToList();
            if (classrooms.Count == 0)
            {
                throw new KeyNotFoundException("No classrooms found to remove.");
            }
            foreach (var classroom in classrooms)
            {
                school.RemoveClassroom(classroom);
            }
            _Context.SaveChanges();
        }
        public void RemoveAllClassroomsFromSchool(int schoolID)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            school.ClearClassrooms();
            _Context.SaveChanges();
        }
        public void AddStudentToSchool(int schoolId, int studentId)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolId);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentId);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }
            school.AddStudent(student);
            _Context.SaveChanges();
        }
        public void RemoveStudentFromSchool(int schoolID, int studentID)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentID);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }
            school.RemoveStudent(student);
            _Context.SaveChanges();
        }
        public void RemoveStudentsFromSchool(int schoolID, List<int> studentIDs)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            List<Student> studentsToRemove = _Context.Students
                .Where(s => studentIDs.Contains(s.ID) && school.Students.Contains(s))
                .ToList();
            if (studentsToRemove.Count == 0)
            {
                throw new KeyNotFoundException("No students found to remove.");
            }
            foreach (var student in studentsToRemove)
            {
                school.RemoveStudent(student);
            }
            _Context.SaveChanges();
        }
        public void RemoveAllStudentsFromSchool(int schoolID)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            school.ClearStudents();
            _Context.SaveChanges();
        }
    }
}
