using ERPAPI.Models;
using ERPAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "订单创建失败");
                return StatusCode(500, new ApiResponse<string>
                {
                    Code = "500",
                    Message = "服务器内部错误",
                    Data = null
                });
            }
        }
    }
} 