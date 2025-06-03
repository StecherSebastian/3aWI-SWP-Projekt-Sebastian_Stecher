using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests
{
    public class DeleteClassroomsRequest
    {
        [Required(ErrorMessage = "List of Classroom IDs is required")]
        [MinLength(2, ErrorMessage = "At least two Classroom IDs must be provided.")]
        public List<int> ClassroomIDs { get; set; } = new List<int>();
    }
}
