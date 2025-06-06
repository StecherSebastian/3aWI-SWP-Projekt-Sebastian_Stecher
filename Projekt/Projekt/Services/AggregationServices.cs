using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Database;

namespace Projekt.Services
{
    public class AggregationServices
    {
        private readonly ProjektDbContext _Context;
        public AggregationServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public int CountClassroomsInSchool(int schoolID)
        {
            School? school = _Context.Schools
                .Include(s => s.Classrooms)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountClassrooms();
        }
        public List<(string, int)> GetClassroomsWithStudentCount(int schoolID)
        {
            School? school = _Context.Schools
                .Include(s => s.Classrooms)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.GetClassroomsWithStudentCount();
        }
        public int CountStudentsInSchool(int schoolID)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountStudents();
        }
        public int CountStudentsByGender(int schoolID, Person.Genders gender)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountStudentsByGender(gender);
        }
        public int CountStudentsInSchoolclass(int schoolID, Student.Schoolclasses schoolclass)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountStudentsBySchoolclass(schoolclass);
        }
        public int CountStudentsInSchoolclassByGender(int schoolID, Student.Schoolclasses schoolclass, Person.Genders gender)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountStudentsInSchoolclassByGender(schoolclass, gender);
        }
        public int CountStudentsInTrack(int schoolID, Student.Tracks track)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountStudentsByTrack(track);
        }
        public int CountStudentsInTrackByGender(int schoolID, Student.Tracks track, Person.Genders gender)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.CountStudentsInTrackByGender(track, gender);
        }
        public double GetAverageAgeOfStudents(int schoolID)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.GetAverageAgeOfStudents();
        }
        public double GetGenderPercentageInSchoolclass(int schoolID, Person.Genders gender, Student.Schoolclasses schoolclass) 
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.GetGenderPercentageInSchoolclass(gender, schoolclass);
        }
        public double GetGenderPercentageInTrack(int schoolID, Person.Genders gender, Student.Tracks track)
        {
            School? school = _Context.Schools
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null) throw new KeyNotFoundException("School not found.");
            return school.GetGenderPercentageInTrack(gender, track);
        }
    }
}
