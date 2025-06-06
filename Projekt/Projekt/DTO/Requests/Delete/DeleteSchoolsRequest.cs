using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests.Delete
{
    public class DeleteSchoolsRequest
    {
        [Required(ErrorMessage = "List of School IDs is required")]
        [MinLength(2, ErrorMessage = "At least two School ID must be provided.")]
        public List<int> SchoolIDs { get; set; } = new List<int>();
    }
}
