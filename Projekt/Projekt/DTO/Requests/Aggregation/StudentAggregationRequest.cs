using System.ComponentModel.DataAnnotations;
using Projekt.Models;

namespace Projekt.DTO.Requests.Aggregation
{
    public class StudentAggregationRequest
    {
        public Person.Genders Gender { get; set; }
        public Student.Schoolclasses Schoolclass { get; set; }
        public Student.Tracks Track { get; set; }
    }
}
