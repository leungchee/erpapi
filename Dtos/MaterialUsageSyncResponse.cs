namespace ERPAPI.Dtos
{
    /// <summary>
    /// 原材料消耗同步接口响应对象
    /// </summary>
    public class MaterialUsageSyncResponse
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 同步失败的订单列表
        /// </summary>
        public List<long> FailedOrders { get; set; } = new List<long>();
    }
} 