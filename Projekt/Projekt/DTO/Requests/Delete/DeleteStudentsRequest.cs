using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests.Delete
{
    public class DeleteStudentsRequest
    {
        [Required]
        [MinLength(2, ErrorMessage = "At least two student IDs must be provided.")]
        public List<int> StudentIDs { get; set; } = new List<int>();
    }
}
