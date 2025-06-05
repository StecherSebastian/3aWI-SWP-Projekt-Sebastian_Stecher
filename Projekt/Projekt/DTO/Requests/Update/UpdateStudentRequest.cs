using System.ComponentModel.DataAnnotations;
using Projekt.Models;

namespace Projekt.DTO.Requests.Update
{
    public class UpdateStudentRequest
    {
        [MinLength(3, ErrorMessage = "Firstname must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Firstname cannot be longer than 100 characters.")]
        public string FirstName { get; set; } = string.Empty;
        [MinLength(3, ErrorMessage = "Lastname must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Lastname cannot be longer than 100 characters.")]
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Person.Genders Gender { get; set; }
        public Student.Schoolclasses Schoolclass { get; set; }
        public Student.Tracks Track { get; set; }
    }
}
