using System.ComponentModel.DataAnnotations;
using Projekt.Models;

namespace Projekt.DTO.Requests.Aggregation
{
    public class StudentAggregationRequest
    {
        [Required(ErrorMessage = "SchoolID is required.")]
        public int SchoolID { get; set; }
        public Person.Genders Gender { get; set; }
        public Student.Schoolclasses Schoolclass { get; set; }
        public Student.Tracks Track { get; set; }
    }
}
