using System.Collections.Generic;
using System.Threading.Tasks;
using ERPAPI.Dtos;

namespace ERPAPI.Services
{
    /// <summary>
    /// 原材料消耗服务接口
    /// </summary>
    public interface IMaterialUsageService
    {
        /// <summary>
        /// 同步原材料消耗信息
        /// </summary>
        /// <param name="dtos">原材料消耗信息列表</param>
        /// <returns>同步结果</returns>
        Task<MaterialUsageSyncResponse> SyncMaterialUsageAsync(List<MaterialUsageSyncDto> dtos);
    }
} 