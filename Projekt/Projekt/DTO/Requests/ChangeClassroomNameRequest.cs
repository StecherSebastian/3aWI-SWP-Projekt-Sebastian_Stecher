using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests
{
    public class ChangeClassroomNameRequest
    {
        [Required(ErrorMessage = "Classroom ID is required.")]
        public int ID { get; set; }
        [Required(ErrorMessage = "Classroom Name is required")]
        [MinLength(3, ErrorMessage = "Classroom Name must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Classroom Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
