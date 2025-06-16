using ERPAPI.Models;

namespace ERPAPI.Services
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync(OrderPushRequest request);
    }
} 