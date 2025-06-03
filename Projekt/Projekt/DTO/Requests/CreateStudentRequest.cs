using System.ComponentModel.DataAnnotations;
using Projekt.Models;

namespace Projekt.DTO.Requests
{
    public class CreateStudentRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Firstname must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Firstname cannot be longer than 100 characters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Lastname must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Lastname cannot be longer than 100 characters.")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public Person.Genders Gender { get; set; }
        [Required]
        public Student.Schoolclasses Schoolclass { get; set; }
        [Required]
        public Student.Tracks Track { get; set; }
    }
}
