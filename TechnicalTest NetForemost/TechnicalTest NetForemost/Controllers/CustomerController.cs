using Microsoft.AspNetCore.Mvc;
using TechnicalTest_NetForemost.DTO;

namespace TechnicalTest_NetForemost.Controllers
{
    [ApiController]
    [Route("Customers/api")]
    public class CustomerController : Controller
    {
        [HttpPost("CreateCustomer")]
        public IActionResult CreateDebtCollectors([FromBody] CustomerDto dto)
        {
            string result = dto.CreateCustomer();
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { Message = "Successfully added." });
            }
            else { return BadRequest(new { Message = result }); }
        }

        [HttpPut("UpdateCustomer/{id}")]
        public IActionResult UpdateCustomer([FromBody] CustomerDto dto, int id)
        {

            string result = dto.UpdateCustomer(id);
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { Message = "Updated successfully.", result = dto.GetByIdCustomer(id) });
            }
            else { return BadRequest(new { Message = result }); }
        }

        [HttpGet("GetByIdCustomer/{id}")]
        public IActionResult GetByIDCustomer(int id)
        {
            CustomerDto dto = new CustomerDto();
            var result = dto.GetByIdCustomer(id);
            if (result != null)
            {
                return Ok(new { Message = "Found successfully.", Result = result });
            }
            else { return BadRequest(new { Message = "Object not found." }); }
        }

        [HttpGet("GetAllCustomers/{offSet}/{pageSize}")]
        public IActionResult GetAllCustomers(int offSet, int pageSize)
        {
            CustomerDto dto = new CustomerDto();
            var result = dto.GetAllCustomer(offSet, pageSize);
            if (result.Count > 0)
            {
                return Ok(new { Message = "Found successfully.", Result = result });
            }
            else { return BadRequest(new { Message = "Objects not found." }); }
        }
    }
}
