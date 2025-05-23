namespace Projekt.Models
{
    public class Student : Person
    {
        public enum Schoolclasses
        {
            Class1a = 0,
            Class1b = 1,
            Class2a = 1,
            Class2b = 2,
            Class3a = 2,
            Class3b = 3,
            Class4a = 3,
            Class4b = 4,
            Class5a = 4,
            Class5b = 5,
        }
        public enum Tracks
        {
            WI = 0,
            WL = 1,
            WM = 2,
            WP = 3,
            CB = 4,
            CT = 5,
            MD = 6,
            MP = 7,
        }
        private Schoolclasses _Schoolclass;
        public Schoolclasses Schoolclass { get { return _Schoolclass; } }
        private Tracks _Track;
        public Tracks Track { get { return _Track; } }
        public Student(string firstName, string lastName, DateTime birthdate, Genders gender, Schoolclasses schoolclass, Tracks track) : base(firstName, lastName, birthdate, gender) 
        {
            _Schoolclass = schoolclass;
            _Track = track;
        }
        public void ChangeSchoolclass(Schoolclasses schoolclass)
        {
           _Schoolclass = schoolclass;
        }
        public void ChangeTrack(Tracks track)
        {
            _Track = track;
        }
    }
}
