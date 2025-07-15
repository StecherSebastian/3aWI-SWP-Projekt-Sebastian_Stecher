using Microsoft.AspNetCore.Mvc;
using Projekt.Services;

namespace Projekt.Controllers.Relations
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomRelationsController : BaseController
    {
        private readonly ClassroomRelationsServices _Services;
        public ClassroomRelationsController(ClassroomRelationsServices services) : base()
        {
            _Services = services;
        }
        [HttpPut("classroom/{classroomID:int}/student/{studentID:int}")]
        public IActionResult AddStudentsToClassroom(int studentID, int classroomID)
        {
            try
            {
                _Services.AddStudentToClassroom(classroomID, studentID);
                return Ok("Student added to Classroom successfully.");
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
        [HttpDelete("classroom/{classroomID:int}/student/{studentID:int}")]
        public IActionResult RemoveStudentFromClassroom(int studentID, int classroomID)
        {
            try
            {
                _Services.RemoveStudentFromClassroom(classroomID, studentID);
                return Ok("Student removed from Classroom successfully.");
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
        [HttpDelete("classroom/{classroomID:int}/students")]
        public IActionResult RemoveStudentsFromClassroom(int classroomID, [FromBody] List<int> studentIDs)
        {
            try
            {
                _Services.RemoveStudentsFromClassroom(classroomID, studentIDs);
                return Ok("Students removed from Classroom successfully.");
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
        [HttpDelete("classroom/{classroomID:int}/students/all", Name = "RemoveAllStudentsFromClassroom")]
        public IActionResult RemoveAllStudentsFromClassroom(int classroomID)
        {
            try
            {
                _Services.RemoveAllStudentsFromClassroom(classroomID);
                return Ok("All Students from Classroom removed.");
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