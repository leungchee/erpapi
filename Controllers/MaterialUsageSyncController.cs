using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialUsageSyncController : ControllerBase
    {
        [HttpGet("material-usage")]
        public IActionResult GetMaterialUsageData([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            // 模拟数据
            var materialUsageData = new[]
            {
                new
                {
                    Id = 1,
                    ProductionOrder = "PROD001",
                    MaterialCode = "M001",
                    MaterialName = "原材料A",
                    UsedQuantity = 100,
                    Unit = "KG",
                    UsageDate = DateTime.Now.AddDays(-1),
                    Department = "生产部"
                },
                new
                {
                    Id = 2,
                    ProductionOrder = "PROD002",
                    MaterialCode = "M002",
                    MaterialName = "原材料B",
                    UsedQuantity = 50,
                    Unit = "PCS",
                    UsageDate = DateTime.Now,
                    Department = "生产部"
                }
            };

            return Ok(materialUsageData);
        }
    }
} 