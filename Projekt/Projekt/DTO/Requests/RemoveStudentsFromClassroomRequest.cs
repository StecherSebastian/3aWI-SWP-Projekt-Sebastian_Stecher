using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests
{
    public class RemoveStudentsFromClassroomRequest
    {
        [Required(ErrorMessage = "ClassroomID is required.")]
        public int ClassroomID { get; set; }
        [Required(ErrorMessage = "List of StudentIDs is required.")]
        [MinLength(2, ErrorMessage = "At least two student IDs must be provided.")]
        public List<int> StudentIDs { get; set; } = new List<int>();
    }
}
