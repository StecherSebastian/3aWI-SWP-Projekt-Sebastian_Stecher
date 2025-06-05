using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Utilities;
using Projekt.Database;
using Projekt.DTO.Requests;
using Projekt.DTO.Requests.Create;
using Projekt.DTO.Requests.Delete;
using Projekt.DTO.Requests.Update;
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
        [HttpPut("updateSchool", Name = "UpdateSchool")]
        public IActionResult UpdateSchool([FromBody] UpdateSchoolRequest request)
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
        [HttpPut("addClassroom/{classroomID:int}/ToSchool/{schoolID:int}", Name = "AddClassroomToSchool")]
        public IActionResult AddClassroomToSchool(int classroomID, int schoolID)
        {
            try
            {
                School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == classroomID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                if (school.Classrooms.Contains(classroom))
                {
                    return NoContent();
                }
                school.AddClassroom(classroom);
                _Context.SaveChanges();
                return Ok("Classroom added to School successfully.");
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeClassroom/{classroomID:int}/fromSchool/{schoolID:int}")]
        public IActionResult RemoveClassroomFromSchool(int classroomID, int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == classroomID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                if (school.Classrooms.Contains(classroom))
                {
                    school.RemoveClassroom(classroom);
                    _Context.SaveChanges();
                    return Ok("Classroom removed from School successfully.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeClassroomsFromSchool", Name = "RemoveClassroomsFromSchool")]
        public IActionResult RemoveClassroomsFromSchool([FromBody] RemoveClassroomsFromSchoolRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                List<Classroom> classrooms = _Context.Classrooms.Where(c => request.ClassroomIDs.Contains(c.ID)).ToList();
                if (classrooms.Count == 0)
                {
                    return NotFound("No classrooms found with the provided IDs.");
                }
                List<int> removed = new();
                List<int> notInSchool = new();
                foreach (var classroom in classrooms)
                {
                    if (school.Classrooms.Contains(classroom))
                    {
                        school.RemoveClassroom(classroom);
                        removed.Add(classroom.ID);
                    }
                    else
                    {
                        notInSchool.Add(classroom.ID);
                    }
                }
                _Context.SaveChanges();
                return Ok(new
                {
                    RemovedClassroomIDs = removed,
                    NotInSchool = notInSchool,
                    Message = removed.Any() ? "Some or all classrooms removed successfully." : "No classrooms were removed. None were in the school."
                });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeAllClassroomsFromSchool/{schoolID:int}", Name = "RemoveAllClassroomsFromSchool")]
        public IActionResult RemoveAllClassroomsFromSchool(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                if (!school.Classrooms.Any())
                {
                    return NoContent();
                }
                else
                {
                    school.ClearClassrooms();
                    _Context.SaveChanges();
                    return Ok("All Classrooms from School removed.");
                }
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("countClassroomsInSchool/{schoolID:int}", Name = "CountClassroomsInSchool")]
        public IActionResult CountClassrooms(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("No schools found.");
                }
                int count = school.CountClassrooms();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getClassroomsWithCynapInSchool/{schoolID:int}", Name = "GetClassroomsWithCynapInSchool")]
        public IActionResult GetClassroomsWithCynapInSchool(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("No schools found.");
                }
                List<string> classroomsWithCynap = school.GetClassroomsWithCynap();
                return Ok(classroomsWithCynap);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("getClassroomsWithStudentCountInSchool/{schoolID:int}", Name = "GetClassroomsWithStudentCountInSchool")]
        public IActionResult GetClassroomsWithStudentCountInSchool(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Classrooms).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("No schools found.");
                }
                List<(string, int)> classroomsWithStudentCount = school.GetClassroomsWithStudentCount();
                return Ok(classroomsWithStudentCount);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPut("addStudent/{studentID:int}/toSchool/{schoolID:int}", Name = "AddStudentToSchool")]
        public IActionResult AddStudentToSchool(int studentID, int schoolID)
        {
            try
            {
                School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentID);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                if (school.Students.Contains(student))
                {
                    return NoContent();
                }
                school.AddStudent(student);
                _Context.SaveChanges();
                return Ok("Student added to School successfully.");
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeStudent/{studentID:int}/fromSchool/{schoolID:int}", Name = "RemoveStudentFromSchool")]
        public IActionResult RemoveStudentFromSchool(int studentID, int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentID);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                if (school.Students.Contains(student))
                {
                    school.RemoveStudent(student);
                    _Context.SaveChanges();
                    return Ok("Student from School removed.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeStudentsFromSchool", Name = "RemoveStudentsFromSchool")]
        public IActionResult RemoveStudentsFromSchool([FromBody] RemoveStudentsFromSchoolRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                List<Student> students = _Context.Students.Where(s => request.StudentIDs.Contains(s.ID)).ToList();
                if (students.Count == 0)
                {
                    return NotFound("No students found with the provided IDs.");
                }
                List<int> removed = new();
                List<int> notInSchool = new();
                foreach (var student in students)
                {
                    if (school.Students.Contains(student))
                    {
                        school.RemoveStudent(student);
                        removed.Add(student.ID);
                    }
                    else
                    {
                        notInSchool.Add(student.ID);
                    }
                }
                _Context.SaveChanges();
                return Ok(new
                {
                    RemovedStudentIDs = removed,
                    NotInSchool = notInSchool,
                    Message = removed.Any() ? "Some or all students removed successfully." : "No students were removed. None were in the school."
                });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeAllStudentsFromSchool/{schoolID:int}", Name = "RemoveAllStudentsFromSchool")]
        public IActionResult RemoveAllStudentsFromSchool(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found");
                }
                if (!school.Students.Any())
                {
                    return NoContent();
                }
                else
                {
                    school.ClearStudents();
                    _Context.SaveChanges();
                    return Ok("All Students from School removed.");
                }
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("countStudentsInSchool/{schoolID:int}", Name = "CountStudents")]
        public IActionResult CountStudents(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("No schools found.");
                }
                int count = school.CountStudents();
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("countStudentsByGenderInSchool/", Name = "CountStudentsByGender")]
        public IActionResult CountStudentsByGenderInSchool([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                var genderCounts = school.CountStudentsByGender(request.Gender);
                return Ok(genderCounts);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("countStudentsInSchoolclass", Name = "CountStudentsInSchoolclass")]
        public IActionResult CountStudentsBySchoolclassInSchool([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Classrooms).ThenInclude(c => c.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                int count = school.CountStudentsBySchoolclass(request.Schoolclass);
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // CountStudentsInSchoolclassByGender
        [HttpGet("countStudentsBySchoolclassAndGenderInSchool", Name = "CountStudentsBySchoolclassAndGenderInSchool")]
        public IActionResult CountStudentsByGenderAndSchoolclassInSchoolclass([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(c => c.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                int count = school.CountStudentsInSchoolclassByGender(request.Schoolclass, request.Gender);
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // CountStudentsByTrack
        [HttpGet("countStudentsByTrackInSchool", Name = "CountStudentsByTrackInSchool")]
        public IActionResult CountStudentsByTrackInSchool([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                int count = school.CountStudentsByTrack(request.Track);
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // CountStudentsInTrackByGender
        [HttpGet("countStudentsInTrackByGenderInSchool", Name = "CountStudentsInTrackByGenderInSchool")]
        public IActionResult CountStudentsByTrackAndGenderInSchool([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                int count = school.CountStudentsInTrackByGender(request.Track, request.Gender);
                return Ok(new { Count = count });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // GetAverageAgeOfStudents
        [HttpGet("getAverageAgeOfStudentsInSchool/{schoolID:int}", Name = "GetAverageAgeOfStudentsInSchool")]
        public IActionResult GetAverageAgeOfStudentsInSchool(int schoolID)
        {
            try
            {
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == schoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                double averageAge = school.GetAverageAgeOfStudents();
                return Ok(new { AverageAge = averageAge });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // GetGenderPercentageInSchoolclass
        [HttpGet("getGenderPercentageInSchoolclass", Name = "GetGenderPercentageInSchoolclass")]
        public IActionResult GetGenderPercantageInSchoolclassInSchool([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                double genderPercentage = school.GetGenderPercentageInSchoolclass(request.Gender, request.Schoolclass);
                return Ok(genderPercentage);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        // GetGenderPercentageInTrack
        [HttpGet("getGenderPercentageInTrack", Name = "GetGenderPercentageInTrack")]
        public IActionResult GetGenderPercentageInTrackInSchool([FromBody] StudentAggregationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                School? school = _Context.Schools.Include(s => s.Students).FirstOrDefault(s => s.ID == request.SchoolID);
                if (school == null)
                {
                    return NotFound("School not found.");
                }
                double percentage = school.GetGenderPercentageInTrack(request.Gender, request.Track);
                return Ok(percentage);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPost("createClassroom", Name = "CreateClassroom")]
        public IActionResult CreateClassroom([FromBody] CreateClassroomRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Validator.ValidNumberOfSeats(request.Size, request.Seats);
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
                if (classroom == null)
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
        [HttpPut("updateClassroom", Name = "UpdateClassroom")]
        public IActionResult ChangeClassroomName([FromBody] UpdateClassroomRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Validator.ValidNumberOfSeats(request.Size, request.Seats);
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == request.ID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                classroom.ChangeName(request.Name);
                classroom.ChangeSize(request.Size);
                classroom.ChangeSeatsCount(request.Seats);
                classroom.ChangeCynap(request.Cynap);
                _Context.SaveChanges();
                return Ok(classroom);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPut("addStudentsToClassroom", Name = "AddStudentsToClassroom")]
        public IActionResult AddStudentsToClassroom([FromBody] AddStudentsToClassroomRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(c => c.ID == request.ClassroomID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == request.StudentID);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                classroom.AddStudent(student);
                _Context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeStudent/{studentID:int}/fromClassroom/{classroomID:int}/")]
        public IActionResult RemoveStudentFromClassroom(int studentID, int classroomID)
        {
            try
            {
                Classroom? classroom = _Context.Classrooms.Include(s => s.Students).FirstOrDefault(s => s.ID == classroomID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == studentID);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                if (classroom.Students.Contains(student))
                {
                    classroom.RemoveStudent(student);
                    _Context.SaveChanges();
                    return Ok("Student from Classroom removed.");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeStudentsFromClassroom", Name = "RemoveStudentsFromClassroom")]
        public IActionResult RemoveStudentsFromClassroom([FromBody] RemoveStudentsFromClassroomRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Classroom? classroom = _Context.Classrooms.Include(s => s.Students).FirstOrDefault(c => c.ID == request.ClassroomID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                List<Student> students = _Context.Students.Where(s => request.StudentIDs.Contains(s.ID)).ToList();
                if (students.Count == 0)
                {
                    return NotFound("No students found with the provided IDs.");
                }
                List<int> removed = new();
                List<int> notInClassroom = new();
                foreach (var student in students)
                {
                    if (classroom.Students.Contains(student))
                    {
                        classroom.RemoveStudent(student);
                        removed.Add(student.ID);
                    }
                    else
                    {
                        notInClassroom.Add(student.ID);
                    }
                }
                return Ok(new
                {
                    RemovedStudentIDs = removed,
                    NotInClassroom = notInClassroom,
                    Message = removed.Any() ? "Some or all students removed successfully." : "No students were removed. None were in the classroom."
                });
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpDelete("removeAllStudentsFromClassroom/{classroomID:int}", Name = "RemoveAllStudentsFromClassroom")]
        public IActionResult RemoveAllStudentsFromClassroom(int classroomID)
        {
            try
            {
                Classroom? classroom = _Context.Classrooms.FirstOrDefault(s => s.ID == classroomID);
                if (classroom == null)
                {
                    return NotFound("Classroom not found");
                }
                classroom.ClearStudents();
                _Context.SaveChanges();
                return Ok("All Students from Classroom removed.");
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }

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
                    value: student.FirstName
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
        [HttpPut("updateStudent", Name = "UpdateStudent")]
        public IActionResult UpdateStudent([FromBody] UpdateStudentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Student? student = _Context.Students.FirstOrDefault(s => s.ID == request.ID);
                if (student == null)
                {
                    return NotFound("Student not found");
                }
                student.ChangeFirstName(request.FirstName);
                student.ChangeLastName(request.LastName);
                student.ChangeBirthdate(request.BirthDate);
                student.ChangeGender(request.Gender);
                student.ChangeSchoolclass(request.Schoolclass);
                student.ChangeTrack(request.Track);
                _Context.SaveChanges();
                return Ok(student);
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