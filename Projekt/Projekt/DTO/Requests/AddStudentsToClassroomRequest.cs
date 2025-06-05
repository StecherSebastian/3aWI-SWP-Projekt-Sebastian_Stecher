using System.ComponentModel.DataAnnotations;

namespace Projekt.DTO.Requests
{
    public class AddStudentsToClassroomRequest
    {
        [Required(ErrorMessage = "An ID of a Classroom is required.")]
        public int ClassroomID { get; set; }
        [Required(ErrorMessage = "An ID of a Student is required.")]
        public int StudentID { get; set; }
    }
}
