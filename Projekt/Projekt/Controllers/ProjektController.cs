using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Projekt.Database;
using Projekt.DTO.Requests;
using Projekt.Models;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjektController : ControllerBase
    {
        private readonly ProjektDbContext _Context;
        public ProjektController(ProjektDbContext context)
        {
            _Context = context;
        }
        [HttpPost("createSchool", Name = "CreateSchool")]
        public IActionResult CreateSchool([FromBody] CreateSchoolRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School school = new School(request.Name);
                _Context.Schools.Add(school);
                _Context.SaveChanges();
                return CreatedAtRoute(
                    routeName: "GetSchoolById",
                    routeValues: new { id = school.ID },
                    value: school.Name
                );
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getSchool/{id:int}", Name = "GetSchoolById")]
        public IActionResult GetSchool(int id)
        {
            try
            {
                School? school = _Context.Schools.FirstOrDefault(s => s.ID == id);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                school = _Context.Schools.Include(s => s.Classrooms).Include(s => s.Students).FirstOrDefault(s => s.ID == id);
                return Ok(school);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getSchools", Name = "GetSchools")]
        public IActionResult GetSchools()
        {
            try
            {
                List<School> schools = _Context.Schools.ToList();
                return Ok(schools);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("deleteSchool/{id:int}", Name = "DeleteSchool")]
        public IActionResult DeleteSchool(int id)
        {
            try
            {
                School? school = _Context.Schools.FirstOrDefault(s => s.ID == id);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                _Context.Schools.Remove(school);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("deleteSchools", Name = "DeleteSchools")]
        public IActionResult DeleteSchools([FromBody] DeleteSchoolsRequest request)
        {
            try
            {
                List<School> schools = _Context.Schools.Where(s => request.IDs.Contains(s.ID)).ToList();
                _Context.Schools.RemoveRange(schools);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPut("changeSchoolName", Name = "ChangeSchoolName")]
        public IActionResult ChangeSchoolName([FromBody] ChangeSchoolNameRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var school = _Context.Schools.FirstOrDefault(s => s.ID == request.ID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                school.ChangeName(request.Name);
                _Context.SaveChanges();
                return Ok(school);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // AddClassroomToSchool endpoint to add a classroom to a school
        // RemoveClassroomFromSchool endpoint to remove a classroom by its ID
        // AddStudentToSchool endpoint to add a student to a school
        // RemoveStudentFromSchool endpoint to remove a student by its ID
        // Add Endpoints for various functions in the school class


        [HttpPost("createClassroom", Name = "CreateClassroom")]
        public IActionResult CreateClassroom([FromBody] CreateClassroomRequest request)
        {
            try
            {
                if (request.Size * 0.75! <= request.Seats)
                {
                    return ValidationProblem("Number of seats must be a maximum of 75% of room size.");
                }
                Classroom classroom = new Classroom(request.Name, request.Size, request.Seats, request.Cynap);
                _Context.Classrooms.Add(classroom);
                _Context.SaveChanges();
                return CreatedAtRoute(
                    routeName: "GetSchoolById",
                    routeValues: new { id = classroom.ID },
                    value: classroom.Name
                );
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getClassroom/{id:int}", Name = "GetClassroom")]
        public IActionResult GetClassroom(int id)
        {
            try
            {
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(s => s.ID == id);
                if ( classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                classroom = _Context.Classrooms.Include(s => s.Students).FirstOrDefault(s => s.ID == id);
                return Ok(classroom);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // GetClassrooms endpoint to retrieve all classrooms in a school
        // RemoveClassroom endpoint to delete a classroom by its ID
        // RemoveClassrooms endpoint to delete classrooms by a list of their IDs
        // AddStudent endpoint to add a student to a classroom

        // CreateStudent endpoint to create a new student
        // GetStudent endpoint to retrieve a student by its ID
        // GetStudents endpoint to retrieve all students in a school
        // RemoveStudent endpoint to delete a student by its ID
        // RemoveStudents endpoint to delete students by a list of their IDs
        private IActionResult HandleInternalError(Exception ex)
        {
            // Optional: log exception here
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}
