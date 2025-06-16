using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPAPI.Dtos;
using ERPAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("openApi/thirdApi")]
    public class MaterialUsageSyncController : ControllerBase
    {
        private readonly IMaterialUsageService _materialUsageService;

        public MaterialUsageSyncController(IMaterialUsageService materialUsageService)
        {
            _materialUsageService = materialUsageService ?? throw new ArgumentNullException(nameof(materialUsageService));
        }

        /// <summary>
        /// 同步原材料消耗信息
        /// </summary>
        /// <param name="dtos">原材料消耗信息列表</param>
        /// <returns>同步结果</returns>
        [HttpPost("acceptUseData")]
        [ProducesResponseType(typeof(MaterialUsageSyncResponse), 200)]
        [ProducesResponseType(typeof(MaterialUsageSyncResponse), 400)]
        [ProducesResponseType(typeof(MaterialUsageSyncResponse), 500)]
        public async Task<ActionResult<MaterialUsageSyncResponse>> SyncMaterialUsage([FromBody] List<MaterialUsageSyncDto> dtos)
        {
            if (dtos == null || !dtos.Any())
            {
                return BadRequest(new MaterialUsageSyncResponse 
                { 
                    Code = "400",
                    Message = "请求数据不能为空",
                    Success = false
                });
            }

            try
            {
                var result = await _materialUsageService.SyncMaterialUsageAsync(dtos);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                // 记录异常日志
                return StatusCode(500, new MaterialUsageSyncResponse
                {
                    Code = "500",
                    Message = $"同步原材料消耗信息时发生错误: {ex.Message}",
                    Success = false
                });
            }
        }

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