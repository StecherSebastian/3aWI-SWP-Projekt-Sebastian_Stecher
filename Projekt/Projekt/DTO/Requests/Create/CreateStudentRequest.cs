using System.ComponentModel.DataAnnotations;
using Projekt.Models;

namespace Projekt.DTO.Requests.Create
{
    public class CreateStudentRequest
    {
        [Required(ErrorMessage = "FirstName is required")]
        [MinLength(3, ErrorMessage = "Firstname must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Firstname cannot be longer than 100 characters.")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "LastName is required")]
        [MinLength(3, ErrorMessage = "Lastname must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Lastname cannot be longer than 100 characters.")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Person.Genders Gender { get; set; }
        [Required(ErrorMessage = "Schoolclass is required")]
        public Student.Schoolclasses Schoolclass { get; set; }
        [Required(ErrorMessage = "Track is required")]
        public Student.Tracks Track { get; set; }
    }
}
