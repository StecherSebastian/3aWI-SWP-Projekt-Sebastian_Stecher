using Microsoft.EntityFrameworkCore;
using Projekt.Database;
using Projekt.Models;

namespace Projekt.Services
{
    public class QueryServices
    {
        private readonly ProjektDbContext _Context;
        public QueryServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public List<string> GetClassroomsWithCynapInSchool(int schoolID)
        {
            School? school = _Context.Schools
                .Include(s => s.Classrooms)
                .FirstOrDefault(s => s.ID == schoolID);
            if (school == null)
            {
                throw new KeyNotFoundException("School not found.");
            }
            return school.GetClassroomsWithCynap();
        }
    }
}
