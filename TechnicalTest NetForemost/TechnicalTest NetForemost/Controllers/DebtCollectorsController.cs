using Microsoft.AspNetCore.Mvc;
using TechnicalTest_NetForemost.Data.Repositories;
using TechnicalTest_NetForemost.DTO;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.Controllers
{
    [ApiController]
    [Route("DebtCollectors/api")]
    public class DebtCollectorsController : Controller
    {
        [HttpPost("CreateDebtCollector")]
        public IActionResult CreateDebtCollectors([FromBody] DebtCollectorDto dto) 
        {
            string result = dto.CreateDebtCollector();
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { Message = "Successfully added." });
            }
            else { return BadRequest(new { Message = result }); }
        }

        [HttpPut("UpdateDebtCollector/{id}")]
        public IActionResult UpdateDebtCollectors([FromBody] DebtCollectorDto dto, int id)
        {

            string result = dto.UpdateDebtCollector(id);
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { Message = "Updated successfully.", result = dto.GetByIdDebtCollectorDto(id) });
            }
            else { return BadRequest(new { Message = result }); }
        }

        [HttpGet("GetByIdDebtCollector/{id}")]
        public IActionResult GetByIDDebtCollectors(int id)
        {
            DebtCollectorDto dto = new DebtCollectorDto();
            var result = dto.GetByIdDebtCollectorDto(id);
            if (result != null)
            {
                return Ok(new { Message = "Found successfully.", Result = result });
            }
            else { return BadRequest(new { Message = "Object not found." }); }
        }

        [HttpGet("GetAllDebtCollector/{offSet}/{pageSize}")]
        public IActionResult GetAllDebtCollectors(int offSet, int pageSize)
        {
            DebtCollectorDto dto = new DebtCollectorDto();
            var result = dto.GetAllDebtCollectorPage(offSet, pageSize);
            if (result.Count > 0)
            {
                return Ok(new { Message = "Found successfully.", Result = result });
            }
            else { return BadRequest(new { Message = "Objects not found." }); }
        }
    }
}
