using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<School> schools = _Context.Schools.Where(s => request.SchoolIDs.Contains(s.ID)).ToList();
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
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
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
        [HttpGet("getClassrooms", Name = "GetClassrooms")]
        public IActionResult GetClassrooms()
        {
            try
            {
                List<Classroom> classrooms = _Context.Classrooms.ToList();
                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("deleteClassroom/{id:int}", Name = "DeleteClassroom")]
        public IActionResult DeleteClassroom(int id)
        {
            try
            {
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(s => s.ID == id);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                _Context.Classrooms.Remove(classroom);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("deleteClassrooms", Name = "DeleteClassrooms")]
        public IActionResult DeleteClassrooms([FromBody] DeleteClassroomsRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<Classroom> classrooms = _Context.Classrooms.Where(s => request.ClassroomIDs.Contains(s.ID)).ToList();
                _Context.Classrooms.RemoveRange(classrooms);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // AddStudent endpoint to add a student to a classroom

        [HttpPost("createStudent", Name = "CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Student student = new Student(request.FirstName, request.LastName, request.BirthDate, request.Gender, request.Schoolclass, request.Track);
                _Context.Students.Add(student);
                _Context.SaveChanges();
                return CreatedAtRoute(
                    routeName: "GetStudentById",
                    routeValues: new { id = student.ID },
                    value: (student.FirstName, student.LastName)
                );
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getStudent/{id:int}", Name = "GetStudentById")]
        public IActionResult GetStudent(int id)
        {
            try
            {
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == id);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getStudents", Name = "GetStudents")]
        public IActionResult GetStudents()
        {
            try
            {
                List<Student> students = _Context.Students.ToList();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("deleteStudent/{id:int}", Name = "DeleteStudent")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == id);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                _Context.Students.Remove(student);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("deleteStudents", Name = "DeleteStudents")]
        public IActionResult DeleteStudents([FromBody] DeleteStudentsRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                List<Student> students = _Context.Students.Where(s => request.StudentIDs.Contains(s.ID)).ToList();
                _Context.Students.RemoveRange(students);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        private IActionResult HandleInternalError(Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}