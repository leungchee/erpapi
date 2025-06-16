using ERPAPI.Data;
using ERPAPI.Models;
using System.Data.SqlClient;

namespace ERPAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<string> CreateOrderAsync(OrderPushRequest request)
        {
            try
            {
                // 验证订单数据
                ValidateOrderRequest(request);

                // 创建订单
                return await _orderRepository.CreateOrderAsync(request);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "数据库操作失败: {ErrorNumber} - {Message}", ex.Number, ex.Message);
                throw; // 让控制器处理具体的错误信息
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建订单失败: {Message}", ex.Message);
                throw; // 让控制器处理具体的错误信息
            }
        }

        private void ValidateOrderRequest(OrderPushRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "订单请求数据不能为空");

            if (string.IsNullOrWhiteSpace(request.OrderSn))
                throw new ArgumentException("订单编号不能为空", nameof(request.OrderSn));

            if (string.IsNullOrWhiteSpace(request.OrderQuantity))
                throw new ArgumentException("订单方量不能为空", nameof(request.OrderQuantity));

            if (string.IsNullOrWhiteSpace(request.DeliveryTime))
                throw new ArgumentException("送达时间不能为空", nameof(request.DeliveryTime));

            if (string.IsNullOrWhiteSpace(request.OrderTime))
                throw new ArgumentException("下单时间不能为空", nameof(request.OrderTime));

            if (string.IsNullOrWhiteSpace(request.PlacerPhone))
                throw new ArgumentException("收货人电话不能为空", nameof(request.PlacerPhone));

            // 验证日期格式
            if (!DateTime.TryParse(request.DeliveryTime, out _))
                throw new ArgumentException("送达时间格式不正确，应为 yyyy-MM-dd HH:mm:ss", nameof(request.DeliveryTime));

            if (!DateTime.TryParse(request.OrderTime, out _))
                throw new ArgumentException("下单时间格式不正确，应为 yyyy-MM-dd HH:mm:ss", nameof(request.OrderTime));

            // 验证手机号格式
            if (!System.Text.RegularExpressions.Regex.IsMatch(request.PlacerPhone, @"^1[3-9]\d{9}$"))
                throw new ArgumentException("收货人电话格式不正确", nameof(request.PlacerPhone));
        }
    }
} 