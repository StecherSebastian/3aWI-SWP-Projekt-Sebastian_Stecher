namespace Projekt.Models
{
    public class School
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        private List<Classroom> _Classrooms = new List<Classroom>();
        public List<Classroom> Classrooms { get { return _Classrooms; } set { _Classrooms = value ?? new List<Classroom>(); } }
        private List<Student> _Students = new List<Student>();
        public List<Student> Students { get { return _Students; } set { _Students = value ?? new List<Student>(); } }
        public School(string name)
        {
            Name = name;
        }
    }
}
