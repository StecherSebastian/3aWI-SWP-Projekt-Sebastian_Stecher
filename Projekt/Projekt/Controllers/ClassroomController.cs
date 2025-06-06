using Microsoft.AspNetCore.Mvc;
using Projekt.DTO.Requests.Create;
using Projekt.DTO.Requests.Delete;
using Projekt.Models;
using Projekt.DTO.Requests.Update;
using Projekt.Services;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : BaseController
    {
        private readonly ClassroomServices _Services;
        public ClassroomController(ClassroomServices services) : base() 
        {
            _Services = services;
        }
        [HttpPost]
        public IActionResult CreateClassroom([FromBody] CreateClassroomRequest request)
        {
            try
            {
                ValidateModelState();
                Classroom classroom = _Services.CreateClassroom(request);
                return CreatedAtRoute(
                    routeName: "GetClassroom",
                    routeValues: new { id = classroom.ID },
                    value: classroom.Name
                );
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("{id:int}", Name = "GetClassroom")]
        public IActionResult GetClassroom(int id)
        {
            try
            {
                Classroom classroom = _Services.GetClassroomByID(id);
                return Ok(classroom);
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
        [HttpGet]
        public IActionResult GetClassrooms()
        {
            try
            {
                List<Classroom> classrooms = _Services.GetAllClassrooms();
                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteClassroom(int id)
        {
            try
            {
                _Services.DeleteClassroom(id);
                return Ok("Classroom deleted successfully.");
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
        [HttpDelete]
        public IActionResult DeleteClassrooms([FromBody] DeleteClassroomsRequest request)
        {
            try
            {
                ValidateModelState();
                _Services.DeletClassrooms(request.ClassroomIDs);
                return Ok("Classrooms deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPut("{classroomID:int}")]
        public IActionResult ChangeClassroomName(int classroomID, [FromBody] UpdateClassroomRequest request)
        {
            try
            {
                Classroom classroom = _Services.UpdateClassroom(classroomID, request);
                return Ok(classroom);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest(aEx.Message);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }

    }
}
