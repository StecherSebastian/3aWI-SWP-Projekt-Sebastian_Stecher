namespace Projekt.Models
{
    public class Classroom : Room
    {
        public int Seats { get; private set; }
        public bool Cynap { get; private set; }
        public List<Student> Students { get; private set; } = new List<Student>();
        public Classroom(string name, int size, int seats, bool cynap) : base(name, size)
        {
            ValidNumberOfSeats(seats);
            Seats = seats;
            Cynap = cynap;
        }
        public void ChangeSeatsCount(int seats)
        {
            ValidNumberOfSeats(seats);
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
        private void ValidNumberOfSeats(int seats)
        {
            const int ratio = 75;
            if (seats < 0 || seats > (Size * ratio / 100) )
            {
                throw new ArgumentOutOfRangeException(nameof(seats), $"Number of seats must be positive and less or equal to {ratio}% of the room size.");
            }
        }
    }
}
