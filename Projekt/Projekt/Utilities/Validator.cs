using Projekt.Models;
using System.Drawing;
using System.Xml.Linq;

namespace Projekt.Utilities
{
    public class Validator
    {
        public static void ValidateString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", paramName);
            }
        }
        public static void ValidateEnumValue<T>(T value, string paramName) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new ArgumentOutOfRangeException(paramName, $"{value} is not a valid value for {typeof(T).Name}.");
        }
        public static ArgumentException? ValidateEnumValueReturnException<T>(T? value, string paramName) where T : struct, Enum
        {
            if (!value.HasValue)
                return new ArgumentNullException(paramName, $"{paramName} is required.");
            if (!Enum.IsDefined(typeof(T), value.Value))
                return new ArgumentOutOfRangeException(paramName, $"{value.Value} is not a valid value for {typeof(T).Name}.");
            return null;
        }
        public static void ValidNumberOfSeats(int? size, int seats)
        {
            const int ratio = 75;
            if (seats < 0 || seats > (size * ratio / 100))
            {
                throw new ArgumentOutOfRangeException(nameof(seats), $"Number of seats must be positive and less or equal to {ratio}% of the room size.");
            }
        }
        public static void ValidateClassroomRequest(Classroom classroom)
        {
            List<ArgumentException> exceptions = new List<ArgumentException>();
            if (string.IsNullOrWhiteSpace(classroom.Name))
                exceptions.Add(new ArgumentNullException(nameof(classroom.Name), "Classroom Name is required."));
            if (classroom.Size == null)
                exceptions.Add(new ArgumentNullException(nameof(classroom.Size), "Classroom Size is required."));
            if (classroom.Size < 20 || classroom.Size > 100)
                exceptions.Add(new ArgumentOutOfRangeException(nameof(classroom.Size), classroom.Size, "Classroom Size must be between 20 and 100."));
            const int ratio = 75;
            if (classroom.Seats == null)
                exceptions.Add(new ArgumentNullException(nameof(classroom.Seats), "Classroom Seats is required"));
            int? maxSeats = classroom.Size * ratio / 100;
            if (classroom.Seats < 0 || classroom.Seats > maxSeats)
                exceptions.Add(new ArgumentOutOfRangeException(nameof(classroom.Seats), classroom.Seats, $"Number of seats must be between 0 and {maxSeats} (which is {ratio}% of room size)."));
            if (classroom.Cynap == null)
                exceptions.Add(new ArgumentNullException(nameof(classroom.Cynap), "Classroom Cynap is required."));
            if (exceptions.Any())
            {
                if (exceptions.Count() > 1)
                    throw new AggregateException("Validation failed with multiple errors.", exceptions);
                throw new AggregateException("Validation failed with following error.", exceptions);
            }
        }
        public static void ValidateStudentRequest(Student student)
        {
            List<ArgumentException> exceptions = new List<ArgumentException>();
            if (string.IsNullOrWhiteSpace(student.FirstName))
                exceptions.Add(new ArgumentNullException(nameof(student.FirstName), "FirstName is required."));
            if (string.IsNullOrWhiteSpace(student.LastName))
                exceptions.Add(new ArgumentNullException(nameof(student.LastName), "LastName is required."));
            if (student.BirthDate == null)
                exceptions.Add(new ArgumentNullException(nameof(student.BirthDate), "BirthDate is required."));
            if (student.BirthDate > DateTime.Now)
                exceptions.Add(new ArgumentOutOfRangeException(nameof(student.BirthDate), "BirthDate cant be in the future."));
            ArgumentException? ex = ValidateEnumValueReturnException(student.Gender, nameof(student.Gender));
            if (ex != null)
                exceptions.Add(ex);
            ex = ValidateEnumValueReturnException(student.Schoolclass, nameof(student.Schoolclass));
            if (ex != null)
                exceptions.Add(ex);
            ex = ValidateEnumValueReturnException(student.Track, nameof(student.Track));
            if (ex != null)
                exceptions.Add(ex);
            if (exceptions.Any())
            {
                if (exceptions.Count() > 1)
                    throw new AggregateException("Validation failed with multiple errors.", exceptions);
                throw new AggregateException("Validation failed with following error.", exceptions);
            }
        }
    }
}
