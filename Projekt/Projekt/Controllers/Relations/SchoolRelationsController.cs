using Microsoft.AspNetCore.Mvc;
using Projekt.Services;

namespace Projekt.Controllers.Relations
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolRelationsController : BaseController
    {
        private readonly SchoolRelationsServices _Service;
        public SchoolRelationsController(SchoolRelationsServices service) : base()
        {
            _Service = service;
        }
        [HttpPut("school/{schoolID:int}/classroom/{classroomID:int}")]
        public IActionResult AddClassroomToSchool(int classroomID, int schoolID)
        {
            try
            {
                _Service.AddClassroomToSchool(schoolID, classroomID);
                return Ok("Classroom added to School successfully.");
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
        [HttpDelete("school/{schoolID:int}/classroom/{classroomID:int}")]
        public IActionResult RemoveClassroomFromSchool(int classroomID, int schoolID)
        {
            try
            {
                _Service.RemoveClassroomFromSchool(schoolID, classroomID);
                return Ok("Classroom from School removed successfully.");
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
        [HttpDelete("school/{schoolID:int}/classrooms")]
        public IActionResult RemoveClassroomsFromSchool(int schoolID, [FromBody] List<int> classroomIDs)
        {
            try
            {
                _Service.RemoveClassroomsFromSchool(schoolID, classroomIDs);
                return Ok("Classrooms removed from School successfully.");
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
        [HttpDelete("school/{schoolID:int}/classrooms/all")]
        public IActionResult RemoveAllClassroomsFromSchool(int schoolID)
        {
            try
            {
               _Service.RemoveAllClassroomsFromSchool(schoolID);
                return Ok("All Classrooms from School removed successfully.");
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

        [HttpPut("school/{schoolID:int}/student/{studentID:int}")]
        public IActionResult AddStudentToSchool(int studentID, int schoolID)
        {
            try
            {
                _Service.AddStudentToSchool(schoolID, studentID);
                return Ok("Student added to School successfully.");
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
        [HttpDelete("school/{schoolID:int}/student/{studentID:int}")]
        public IActionResult RemoveStudentFromSchool(int studentID, int schoolID)
        {
            try
            {
                _Service.RemoveStudentFromSchool(schoolID, studentID);
                return Ok("Student from School removed successfully.");
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
        [HttpDelete("school/{schoolID:int}/students")]
        public IActionResult RemoveStudentsFromSchool(int schoolID, [FromBody] List<int> studentIDs)
        {
            try
            {
                _Service.RemoveStudentsFromSchool(schoolID, studentIDs);
                return Ok("Students removed from School successfully.");
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
        [HttpDelete("school/{schoolID:int}/students/all")]
        public IActionResult RemoveAllStudentsFromSchool(int schoolID)
        {
            try
            {
               _Service.RemoveAllStudentsFromSchool(schoolID);
                return Ok("All Students from School removed successfully.");
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