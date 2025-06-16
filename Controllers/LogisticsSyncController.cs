using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERPAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPAPI.Services;

namespace ERPAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("openApi/thirdErpApi/v2")]
    public class LogisticsSyncController : ControllerBase
    {
        private readonly ILogisticsService _logisticsService;

        public LogisticsSyncController(ILogisticsService logisticsService)
        {
            _logisticsService = logisticsService ?? throw new ArgumentNullException(nameof(logisticsService));
        }

        /// <summary>
        /// 同步物流信息
        /// </summary>
        /// <param name="dtos">物流信息列表</param>
        /// <returns>同步结果</returns>
        [HttpPost("erpTicket")]
        [ProducesResponseType(typeof(LogisticsSyncResponse), 200)]
        [ProducesResponseType(typeof(LogisticsSyncResponse), 400)]
        [ProducesResponseType(typeof(LogisticsSyncResponse), 500)]
        public async Task<ActionResult<LogisticsSyncResponse>> SyncLogistics([FromBody] List<LogisticsSyncDto> dtos)
        {
            if (dtos == null || !dtos.Any())
            {
                return BadRequest(new LogisticsSyncResponse 
                { 
                    Code = "400",
                    Message = "请求数据不能为空",
                    Success = false
                });
            }

            try
            {
                var result = await _logisticsService.SyncLogisticsAsync(dtos);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                // 记录异常日志
                return StatusCode(500, new LogisticsSyncResponse
                {
                    Code = "500",
                    Message = $"同步物流信息时发生错误: {ex.Message}",
                    Success = false
                });
            }
        }

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