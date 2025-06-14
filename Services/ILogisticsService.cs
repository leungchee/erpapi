using System.Collections.Generic;
using System.Threading.Tasks;
using ERPAPI.Dtos;

namespace ERPAPI.Services
{
    /// <summary>
    /// 物流服务接口
    /// </summary>
    public interface ILogisticsService
    {
        /// <summary>
        /// 同步物流信息
        /// </summary>
        /// <param name="dtos">物流信息列表</param>
        /// <returns>同步结果</returns>
        Task<LogisticsSyncResponse> SyncLogisticsAsync(List<LogisticsSyncDto> dtos);
    }
} 