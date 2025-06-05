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
        [HttpGet("getClassroomsWithCynapInSchool/{schoolID:int}", Name = "GetClassroomsWithCynapInSchool")]
        public IActionResult GetClassroomsWithCynapInSchool(int schoolID)
        {
            try
            {
                List<string> classroomsWithCynap = _Services.GetClassroomsWithCynapInSchool(schoolID);
                return Ok(classroomsWithCynap);
            }
            catch (Exception ex)
            {
                return HandleInternalError(ex);
            }
        }

    }
}
