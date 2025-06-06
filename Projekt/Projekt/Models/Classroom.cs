using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Classroom : Room
    {
        [Required(ErrorMessage = "Seats is required.")]
        [Range(0, 75, ErrorMessage = "Number of Seats has to be between 0-75 and also only be a maximum of 75% of room size.")]
        public int Seats { get; private set; }
        [Required(ErrorMessage = "Cynap is required.")]
        public bool Cynap { get; private set; }
        public List<Student> Students { get; private set; } = new List<Student>();
        public Classroom(string name, int size, int seats, bool cynap) : base(name, size)
        {
            Seats = seats;
            Cynap = cynap;
        }
        protected Classroom() : base() { }
        public void ChangeSeatsCount(int seats)
        {
            Utilities.Validator.ValidNumberOfSeats(Size, seats);
            Seats = seats;
        }
        public void ChangeCynap(bool cynap)
        {
            Cynap = cynap;
        }
        public void AddStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            if (Students.Count > Seats)
            {
                throw new InvalidOperationException("Classroom is full.");
            }
            Students.Add(student);
        }
        public void RemoveStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            if (!Students.Remove(student))
            {
                throw new InvalidOperationException("Student not found in classroom.");
            }
        }
        public void ClearStudents()
        {
            Students.Clear();
        }
    }
}
