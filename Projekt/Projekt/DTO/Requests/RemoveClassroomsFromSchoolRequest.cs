using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests
{
    public class RemoveClassroomsFromSchoolRequest
    {
        [Required(ErrorMessage = "SchoolID is required.")]
        public int SchoolID { get; set; }
        [Required(ErrorMessage = "List of ClassroomIDs is required.")]
        [MinLength(2, ErrorMessage = "At least two classroom IDs must be provided.")]
        public List<int> ClassroomIDs { get; set; } = new List<int>();
    }
}
