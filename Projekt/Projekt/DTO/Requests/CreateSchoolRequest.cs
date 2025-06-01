using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests
{
    public class CreateSchoolRequest
    {
        [Required(ErrorMessage = "School Name is required")]
        [MinLength(3, ErrorMessage = "School Name must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "School Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
