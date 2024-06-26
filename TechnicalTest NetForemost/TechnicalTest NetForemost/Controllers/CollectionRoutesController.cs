using Microsoft.AspNetCore.Mvc;
using TechnicalTest_NetForemost.DTO;

namespace TechnicalTest_NetForemost.Controllers
{
    [ApiController]
    [Route("CollectionRoutes/api")]
    public class CollectionRoutesController : Controller
    {
        [HttpGet("GetSumByCollector")]
        public IActionResult GetSumByCollector() 
        {
            CollectionRoutesDto dto = new CollectionRoutesDto();
            var result = dto.GetSumByCollector();
            if (result.Count > 0)
            {
                return Ok(new { Message = "Found successfully.", Result = result });
            }
            else { return BadRequest(new { Message = "Objects not found." }); }
        }
    }
}
