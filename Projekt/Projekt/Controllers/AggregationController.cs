using Microsoft.AspNetCore.Mvc;
using Projekt.Services;
using Projekt.DTO.Requests.Aggregation;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : BaseController
    {
        private readonly AggregationServices _Services;
        public AggregationController(AggregationServices services) : base()
        {
            _Services = services;
        }
        [HttpGet("schools/{schoolID:int}/classrooms/count")]
        public IActionResult CountClassrooms(int schoolID)
        {
            try
            {
                int count = _Services.CountClassroomsInSchool(schoolID);
                return Ok(new { Count = count });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/classrooms/with-student-count")]
        public IActionResult GetClassroomsWithStudentCountInSchool(int schoolID)
        {
            try
            {
                List<(string, int)> classroomsWithStudentCount = _Services.GetClassroomsWithStudentCount(schoolID);
                return Ok(classroomsWithStudentCount);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/count")]
        public IActionResult CountStudents(int schoolID)
        {
            try
            {
                int count = _Services.CountClassroomsInSchool(schoolID);
                return Ok(count);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/count-by-gender")]
        public IActionResult CountStudentsByGenderInSchool(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                int genderCounts = _Services.CountStudentsByGender(schoolID, request.Gender);
                return Ok(genderCounts);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/count-by-schoolclass")]
        public IActionResult CountStudentsBySchoolclassInSchool(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInSchoolclass(schoolID, request.Schoolclass);
                return Ok(count);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/count-by-gender&schoolclass")]
        public IActionResult CountStudentsByGenderAndSchoolclassInSchoolclass(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInSchoolclassByGender(schoolID, request.Schoolclass, request.Gender);
                return Ok(count);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/count-by-track")]
        public IActionResult CountStudentsByTrackInSchool(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInTrack(schoolID, request.Track);
                return Ok(count);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/count-by-gender&track")]
        public IActionResult CountStudentsByTrackAndGenderInSchool(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInTrackByGender(schoolID, request.Track, request.Gender);
                return Ok(new { Count = count });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/average-age")]
        public IActionResult GetAverageAgeOfStudentsInSchool(int schoolID)
        {
            try
            {
                double averageAge = _Services.GetAverageAgeOfStudents(schoolID);
                return Ok(averageAge);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/schoolclasses/gender-percentage")]
        public IActionResult GetGenderPercantageInSchoolclassInSchool(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                double genderPercentage = _Services.GetGenderPercentageInSchoolclass(schoolID, request.Gender, request.Schoolclass);
                return Ok(genderPercentage);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("schools/{schoolID:int}/students/track/gender-percentage")]
        public IActionResult GetGenderPercentageInTrackInSchool(int schoolID, [FromBody] StudentAggregationRequest request)
        {
            try
            {
                ValidateModelState();
                double percentage = _Services.GetGenderPercentageInTrack(schoolID, request.Gender, request.Track);
                return Ok(percentage);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }

    }
}
