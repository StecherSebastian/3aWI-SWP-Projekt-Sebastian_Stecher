using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Services;
using Projekt.Models;

namespace Projekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : BaseController
    {
        private readonly QueryServices _Services;
        public QueryController(QueryServices services) : base() 
        {
            _Services = services;
        }
        [HttpGet("schools/{schoolId:int}/classrooms/with-cynap", Name = "GetClassroomsWithCynapInSchool")]
        public IActionResult GetClassroomsWithCynapInSchool(int schoolID)
        {
            try
            {
                List<string> classroomsWithCynap = _Services.GetClassroomsWithCynapInSchool(schoolID);
                return Ok(classroomsWithCynap);
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
