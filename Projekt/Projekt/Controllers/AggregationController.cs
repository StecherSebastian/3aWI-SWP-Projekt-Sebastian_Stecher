using Microsoft.AspNetCore.Mvc;
using Projekt.Services;
using Projekt.Models;

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
        public IActionResult CountStudentsByGenderInSchool(int schoolID, [FromQuery] Person.Genders gender)
        {
            try
            {
                ValidateModelState();
                int genderCounts = _Services.CountStudentsByGender(schoolID, gender);
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
        public IActionResult CountStudentsBySchoolclassInSchool(int schoolID, [FromQuery] Student.Schoolclasses schoolclass)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInSchoolclass(schoolID, schoolclass);
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
        public IActionResult CountStudentsByGenderAndSchoolclassInSchoolclass(int schoolID, [FromQuery] Student.Schoolclasses schoolclass, [FromQuery] Person.Genders gender)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInSchoolclassByGender(schoolID, schoolclass, gender);
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
        public IActionResult CountStudentsByTrackInSchool(int schoolID, [FromQuery] Student.Tracks track)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInTrack(schoolID, track);
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
        public IActionResult CountStudentsByTrackAndGenderInSchool(int schoolID, [FromQuery] Student.Tracks track, [FromQuery] Person.Genders gender)
        {
            try
            {
                ValidateModelState();
                int count = _Services.CountStudentsInTrackByGender(schoolID, track, gender);
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
        [HttpGet("schools/{schoolID:int}/students/schoolclasses-gender-percentage")]
        public IActionResult GetGenderPercantageInSchoolclassInSchool(int schoolID, [FromQuery] Person.Genders gender, [FromQuery] Student.Schoolclasses schoolclass)
        {
            try
            {
                ValidateModelState();
                double percentage = _Services.GetGenderPercentageInSchoolclass(schoolID, gender, schoolclass);
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
        [HttpGet("schools/{schoolID:int}/students/track-gender-percentage")]
        public IActionResult GetGenderPercentageInTrackInSchool(int schoolID, [FromQuery] Person.Genders gender, [FromQuery] Student.Tracks track)
        {
            try
            {
                ValidateModelState();
                double percentage = _Services.GetGenderPercentageInTrack(schoolID, gender, track);
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