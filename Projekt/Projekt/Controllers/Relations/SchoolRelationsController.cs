using Microsoft.AspNetCore.Mvc;
using Projekt.DTO.Requests.Relations;
using Projekt.Services;

namespace Projekt.Controllers.Relations
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolRelationsController : BaseController
    {
        private readonly SchoolRelationServices _Service;
        public SchoolRelationsController(SchoolRelationServices service) : base()
        {
            _Service = service;
        }
        [HttpPut("{schoolID:int}/classroom/{classroomID:int}")]
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
        [HttpDelete("{schoolID:int}/classroom/{classroomID:int}")]
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
        [HttpDelete("{schoolID:int}/classrooms")]
        public IActionResult RemoveClassroomsFromSchool([FromBody] RemoveClassroomsFromSchoolRequest request, int schoolID)
        {
            try
            {
                _Service.RemoveClassroomsFromSchool(schoolID, request.ClassroomIDs);
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
        [HttpDelete("{schoolID:int}/classrooms/all", Name = "RemoveAllClassroomsFromSchool")]
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

        [HttpPut("{schoolID:int}/student/{studentID:int}")]
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
        [HttpDelete("{schoolID:int}/student/{studentID:int}")]
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
        [HttpDelete("{schoolID:int}/students", Name = "RemoveStudentsFromSchool")]
        public IActionResult RemoveStudentsFromSchool([FromBody] RemoveStudentsFromSchoolRequest request, int schoolID)
        {
            try
            {
                _Service.RemoveStudentsFromSchool(schoolID, request.StudentIDs);
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
        [HttpDelete("{schoolID:int}/students/all", Name = "RemoveAllStudentsFromSchool")]
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