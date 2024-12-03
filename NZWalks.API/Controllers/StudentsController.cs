using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() {
            string[] studentNames = new string[] { "Tom", "Jerry", "Spike" };

            return Ok(studentNames);
        }
    }
}
