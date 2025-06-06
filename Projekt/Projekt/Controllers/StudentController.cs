using Microsoft.AspNetCore.Mvc;
using Projekt.Services;
using Projekt.DTO.Requests.Create;
using Projekt.DTO.Requests.Delete;
using Projekt.DTO.Requests.Update;
using Projekt.Models;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        private readonly StudentServices _Services;
        public StudentController(StudentServices services) : base() 
        {
            _Services = services;
        }
        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
        {
            try
            {
                ValidateModelState();
                Student student = _Services.CreateStudent(request);
                return CreatedAtRoute(
                    routeName: "GetStudent",
                    routeValues: new { id = student.ID },
                    value: student.FirstName
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
        [HttpGet("{id:int}", Name = "GetStudent")]
        public IActionResult GetStudent(int id)
        {
            try
            {
                Student student = _Services.GetStudentByID(id);
                return Ok(student);
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
        public IActionResult GetStudents()
        {
            try
            {
                List<Student> students = _Services.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _Services.DeleteStudent(id);
                return Ok("Student deleted successfully.");
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
        public IActionResult DeleteStudents([FromBody] DeleteStudentsRequest request)
        {
            try
            {
                ValidateModelState();
                _Services.DeleteStudents(request.StudentIDs);
                return Ok("Students deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPut("{studentID:int}")]
        public IActionResult UpdateStudent(int studentID, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                ValidateModelState();
                Student student = _Services.UpdateStudent(studentID, request);
                return Ok(student);
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