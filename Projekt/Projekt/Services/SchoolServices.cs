using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Database;

namespace Projekt.Services
{
    public class SchoolServices
    {
        private readonly ProjektDbContext _Context;
        public SchoolServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public School CreateSchool(School request)
        {
            Utilities.Validator.ValidateString(request.Name, "SchoolName");
            var school = new School(request.Name);
            _Context.Schools.Add(school);
            _Context.SaveChanges();
            return school;
        }
        public School GetSchoolByID(int id)
        {
            School? school = _Context.Schools
                .Include(s => s.Classrooms)
                .Include(s => s.Students)
                .FirstOrDefault(s => s.ID == id);
            if (school == null) throw new KeyNotFoundException("School not found.");               
            return school;
        }
        public List<School> GetAllSchools() =>
            _Context.Schools.ToList();
        public void DeleteSchool(int id)
        {
            var school = _Context.Schools.FirstOrDefault(s => s.ID == id);
            if (school == null) throw new KeyNotFoundException("School not found.");
            _Context.Schools.Remove(school);
            _Context.SaveChanges();
        }
        public void DeleteSchools(List<int> schoolIDs)
        {
            List<School> schools = _Context.Schools.Where(s => schoolIDs.Contains(s.ID)).ToList();
            _Context.Schools.RemoveRange(schools);
            _Context.SaveChanges();
        }
        public School UpdateSchool(int id, School request)
        {
            Utilities.Validator.ValidateString(request.Name, "SchoolName");
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == id);
            if (school == null) throw new KeyNotFoundException("School not found.");
            school.ChangeName(request.Name);
            _Context.SaveChanges();
            return school;
        }
    }

}
