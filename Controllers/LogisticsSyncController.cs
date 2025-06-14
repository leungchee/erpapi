using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LogisticsSyncController : ControllerBase
    {
        [HttpGet("logistics")]
        public IActionResult GetLogisticsData([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            // 模拟数据
            var logisticsData = new[]
            {
                new
                {
                    Id = 1,
                    OrderNumber = "LOG001",
                    DeliveryDate = DateTime.Now.AddDays(1),
                    Status = "Pending",
                    Items = new[]
                    {
                        new { ProductId = "P001", Quantity = 10, UnitPrice = 100.00m }
                    }
                },
                new
                {
                    Id = 2,
                    OrderNumber = "LOG002",
                    DeliveryDate = DateTime.Now.AddDays(2),
                    Status = "In Transit",
                    Items = new[]
                    {
                        new { ProductId = "P002", Quantity = 5, UnitPrice = 200.00m }
                    }
                }
            };

            return Ok(logisticsData);
        }
    }
} 