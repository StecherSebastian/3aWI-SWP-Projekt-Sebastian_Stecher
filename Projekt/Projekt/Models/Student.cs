using Projekt.Utilities;

namespace Projekt.Models
{
    public class Student : Person
    {
        public enum Schoolclasses
        {
            Class1a = 0,
            Class1b = 1,
            Class2a = 2,
            Class2b = 3,
            Class3a = 4,
            Class3b = 5,
            Class4a = 6,
            Class4b = 7,
            Class5a = 8,
            Class5b = 9,
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
        public Schoolclasses Schoolclass { get; private set; }
        public Tracks Track {  get; private set; }
        public Student(string firstName, string lastName, DateTime birthdate, Genders gender, Schoolclasses schoolclass, Tracks track) : base(firstName, lastName, birthdate, gender) 
        {
            Validator.ValidateEnumValue(schoolclass, nameof(schoolclass));
            Schoolclass = schoolclass;
            Validator.ValidateEnumValue(track, nameof(track));
            Track = track;
        }
        public void ChangeSchoolclass(Schoolclasses schoolclass)
        {
            Validator.ValidateEnumValue(schoolclass, nameof(schoolclass));
            Schoolclass = schoolclass;
        }
        public void ChangeTrack(Tracks track)
        {
            Validator.ValidateEnumValue(track, nameof(track));
            Track = track;
        }

    }
}
