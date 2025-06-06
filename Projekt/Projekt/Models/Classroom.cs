﻿using Projekt.Utilities;

namespace Projekt.Models
{
    public class Classroom : Room
    {
        public int Seats { get; private set; }
        public bool Cynap { get; private set; }
        public List<Student> Students { get; private set; } = new List<Student>();
        public Classroom(string name, int size, int seats, bool cynap) : base(name, size)
        {
            Validator.ValidNumberOfSeats(size, seats);
            Seats = seats;
            Cynap = cynap;
        }
        protected Classroom() : base() { }
        public void ChangeSeatsCount(int seats)
        {
            Validator.ValidNumberOfSeats(Size, seats);
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
