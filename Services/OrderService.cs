using ERPAPI.Data;
using ERPAPI.Models;

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建订单失败: {Message}", ex.Message);
                throw;
            }
        }

        private void ValidateOrderRequest(OrderPushRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.OrderSn))
                throw new ArgumentException("订单编号不能为空");

            if (string.IsNullOrWhiteSpace(request.OrderQuantity))
                throw new ArgumentException("订单方量不能为空");

            if (string.IsNullOrWhiteSpace(request.DeliveryTime))
                throw new ArgumentException("送达时间不能为空");

            if (string.IsNullOrWhiteSpace(request.OrderTime))
                throw new ArgumentException("下单时间不能为空");

            if (string.IsNullOrWhiteSpace(request.PlacerPhone))
                throw new ArgumentException("收货人电话不能为空");
        }
    }
} 