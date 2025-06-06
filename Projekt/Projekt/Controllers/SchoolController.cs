using Microsoft.AspNetCore.Mvc;
using Projekt.DTO.Requests.Create;
using Projekt.DTO.Requests.Delete;
using Projekt.DTO.Requests.Update;
using Projekt.Models;
using Projekt.Services;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        private readonly SchoolServices _Service;

        public SchoolController(SchoolServices service) : base()
        {
            _Service = service;
        }
        [HttpPost]
        public IActionResult CreateSchool([FromBody] CreateSchoolRequest request)
        {
            try
            {
                ValidateModelState();
                School school = _Service.CreateSchool(request);
                return CreatedAtRoute(
                    routeName: "GetSchool",
                    routeValues: new { id = school.ID },
                    value: school.Name
                );
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpGet("{id:int}", Name = "GetSchool")]
        public IActionResult GetSchool(int id)
        {
            try
            {
                School school = _Service.GetSchoolByID(id);
                return Ok(school);
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
        public IActionResult GetSchools()
        {
            try
            {
                List<School> schools = _Service.GetAllSchools();
                return Ok(schools);
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
        [HttpDelete("{id:int}")]
        public IActionResult DeleteSchool(int id)
        {
            try
            {
                _Service.DeleteSchool(id);
                return Ok("School deleted successfully.");
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
        public IActionResult DeleteSchools([FromBody] DeleteSchoolsRequest request)
        {
            try
            {
                ValidateModelState();
                _Service.DeleteSchools(request.SchoolIDs);
                return Ok("Schools deleted successfully.");
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateSchool([FromBody] UpdateSchoolRequest request, int id)
        {
            try
            {
                ValidateModelState();
                School? school = _Service.UpdateSchool(id, request);
                return Ok(school);
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