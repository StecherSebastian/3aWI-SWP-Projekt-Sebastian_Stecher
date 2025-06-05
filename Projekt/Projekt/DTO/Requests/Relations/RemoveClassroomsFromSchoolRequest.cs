using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests.Relations
{
    public class RemoveClassroomsFromSchoolRequest
    {
        [Required(ErrorMessage = "List of ClassroomIDs is required.")]
        [MinLength(2, ErrorMessage = "At least two classroom IDs must be provided.")]
        public List<int> ClassroomIDs { get; set; } = new List<int>();
    }
}
