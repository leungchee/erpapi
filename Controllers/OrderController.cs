using ERPAPI.Models;
using ERPAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ERPAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>
        /// 智选推送订单到ERP
        /// </summary>
        /// <param name="request">订单信息</param>
        /// <returns>ERP系统中的计划ID</returns>
        [HttpPost("push")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PushOrder([FromBody] OrderPushRequest request)
        {
            try
            {
                // 创建订单
                var planId = await _orderService.CreateOrderAsync(request);

                return Ok(new ApiResponse<string>
                {
                    Code = "200",
                    Message = "订单创建成功",
                    Data = planId
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "订单数据验证失败");
                return BadRequest(new ApiResponse<string>
                {
                    Code = "400",
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (SqlException ex)
            {
                string errorMessage = ex.Number switch
                {
                    -2 => "数据库连接超时",
                    -1 => "数据库连接失败",
                    4060 => "数据库不存在",
                    18456 => "数据库登录失败",
                    547 => "数据完整性错误，可能是外键约束或唯一性约束冲突",
                    2627 => "违反唯一性约束，订单编号已存在",
                    _ => $"数据库错误: {ex.Message}"
                };

                _logger.LogError(ex, "数据库操作失败: {ErrorMessage}", errorMessage);
                return StatusCode(500, new ApiResponse<string>
                {
                    Code = "500",
                    Message = errorMessage,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "订单创建失败: {Message}", ex.Message);
                return StatusCode(500, new ApiResponse<string>
                {
                    Code = "500",
                    Message = $"系统错误: {ex.Message}",
                    Data = null
                });
            }
        }
    }
} 