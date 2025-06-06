using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Database;
using Projekt.DTO.Requests.Create;
using Projekt.DTO.Requests.Update;

namespace Projekt.Services
{
    public class SchoolServices
    {
        private readonly ProjektDbContext _Context;
        public SchoolServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public School CreateSchool(CreateSchoolRequest request)
        {
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
        public void DeleteSchools(List<int> ids)
        {
            List<School> schools = _Context.Schools.Where(s => ids.Contains(s.ID)).ToList();
            _Context.Schools.RemoveRange(schools);
            _Context.SaveChanges();
        }
        public School UpdateSchool(int id, UpdateSchoolRequest request)
        {
            School? school = _Context.Schools.FirstOrDefault(s => s.ID == id);
            if (school == null) throw new KeyNotFoundException("School not found.");
            school.ChangeName(request.Name);
            _Context.SaveChanges();
            return school;
        }
    }

}
