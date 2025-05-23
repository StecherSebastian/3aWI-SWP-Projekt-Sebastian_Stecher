namespace Projekt.Models
{
    public class Classroom : Room
    {
        private int _Seats;
        public int Seats { get { return _Seats; } }
        private bool _Cynap;
        public bool Cynap { get { return _Cynap; } }
        private List<Student> _Students = new List<Student>();
        public List<Student> Students { get { return _Students; } }
        public Classroom(string name, int size, int seats, bool cynap) : base(name, size)
        {
            _Seats = seats;
            _Cynap = cynap;
        }
        public void ChangeNumberOfSeats(int seats)
        {
            if (seats > 0 && seats <= Size)
            {
                _Seats = seats;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(seats), "Number of seats must be positive and less than or equal to room size.");
            }
        }
        public void ChangeCynap(bool cynap)
        {
            _Cynap = cynap;
        }
        public void AddStudent(Student student)
        {
            if (Students.Count < Seats)
            {
                _Students.Add(student);
            }
            else
            {
                throw new InvalidOperationException("Classroom is full.");
            }
        }
        public void RemoveStudent(Student student)
        {
            if (_Students.Contains(student))
            {
                _Students.Remove(student);
            }
            else
            {
                throw new InvalidOperationException("Student not found in classroom.");
            }
        }
        public void ClearStudents()
        {
            _Students.Clear();
        }
    }
}
