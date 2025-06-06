using Microsoft.EntityFrameworkCore;
using Projekt.Models;
using Projekt.Database;
using Projekt.DTO.Requests.Create;
using Projekt.Utilities;
using Projekt.DTO.Requests.Update;

namespace Projekt.Services
{
    public class ClassroomServices
    {
        private readonly ProjektDbContext _Context;
        public ClassroomServices(ProjektDbContext context)
        {
            _Context = context;
        }
        public Classroom CreateClassroom(CreateClassroomRequest request)
        {
            Classroom classroom = new Classroom(request.Name, request.Size, request.Seats, request.Cynap);
            _Context.Classrooms.Add(classroom);
            _Context.SaveChanges();
            return classroom;
        }
        public Classroom GetClassroomByID(int id)
        {
            Classroom? classroom = _Context.Classrooms
                .Include(c => c.Students)
                .FirstOrDefault(c => c.ID == id);
            if (classroom == null) throw new KeyNotFoundException("Classroom not found");
            return classroom;
        }
        public List<Classroom> GetAllClassrooms() =>
             _Context.Classrooms.ToList();
        public void DeleteClassroom(int id)
        {
            Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == id);
            if (classroom == null) throw new KeyNotFoundException("Classroom not found");
            _Context.Classrooms.Remove(classroom);
            _Context.SaveChanges();
        }
        public void DeletClassrooms(List<int> classroomIds)
        {
            List<Classroom> classrooms = _Context.Classrooms.Where(s => classroomIds.Contains(s.ID)).ToList();
            _Context.Classrooms.RemoveRange(classrooms);
            _Context.SaveChanges();
        }
        public Classroom UpdateClassroom(int id, UpdateClassroomRequest request)
        {
            Validator.ValidNumberOfSeats(request.Size, request.Seats);
            Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == id);
            if (classroom == null) throw new KeyNotFoundException("Classroom not found");
            classroom.ChangeName(request.Name);
            classroom.ChangeSize(request.Size);
            classroom.ChangeSeatsCount(request.Seats);
            classroom.ChangeCynap(request.Cynap);
            _Context.SaveChanges();
            return classroom;
        }
    }
}
