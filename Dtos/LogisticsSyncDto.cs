using System;

namespace ERPAPI.Dtos
{
    /// <summary>
    /// 物流同步数据传输对象
    /// </summary>
    public class LogisticsSyncDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// 物流状态
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// 物流公司代码
        /// </summary>
        public string? LogisticsCompanyCode { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        public string? LogisticsCompanyName { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? ShipmentTime { get; set; }

        /// <summary>
        /// 预计送达时间
        /// </summary>
        public DateTime? EstimatedDeliveryTime { get; set; }

        /// <summary>
        /// 实际送达时间
        /// </summary>
        public DateTime? ActualDeliveryTime { get; set; }

        /// <summary>
        /// 收货人信息
        /// </summary>
        public string? ReceiverInfo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
} 