using Microsoft.AspNetCore.Mvc;
using TechnicalTest_NetForemost.DTO;
using TechnicalTest_NetForemost.Models;

namespace TechnicalTest_NetForemost.Controllers
{
    [ApiController]
    [Route("AccountsReceivable/api")]
    public class AccountsReceivableController : Controller
    {
        [HttpPost("CreateAccountsReceivable")]
        public IActionResult CreateAccountsReceivable([FromBody] AccountsReceivableDto dto)
        {
            string result = dto.CreateAccountsReceivable();
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { Message = "Successfully added." });
            }
            else { return BadRequest(new { Message = result }); }
        }
    }
}
