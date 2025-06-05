using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests.Relations
{
    public class RemoveStudentsFromClassroomRequest
    {
        [Required(ErrorMessage = "List of StudentIDs is required.")]
        [MinLength(2, ErrorMessage = "At least two student IDs must be provided.")]
        public List<int> StudentIDs { get; set; } = new List<int>();
    }
}
