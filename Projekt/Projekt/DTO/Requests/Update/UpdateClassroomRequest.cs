using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests.Update
{
    public class UpdateClassroomRequest
    {
        [MinLength(3, ErrorMessage = "Classroom Name must be at least 3 characters long.")]
        [StringLength(100, ErrorMessage = "Classroom Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
        [Range(20, 100, ErrorMessage = "Classroom Size needs to be between or equal to 20 and 100")]
        public int Size { get; set; }
        [Range(15, 75, ErrorMessage = "Classroom Seats need to be between or equal to 15 and 75 (Max 75% of Room Size)")]
        public int Seats { get; set; }
        public bool Cynap { get; set; }
    }
}
