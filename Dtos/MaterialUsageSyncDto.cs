using System;

namespace ERPAPI.Dtos
{
    /// <summary>
    /// 原材料消耗同步 DTO（根据最新接口文档）
    /// </summary>
    public class MaterialUsageSyncDto
    {
        /// <summary>订单id</summary>
        public long orderId { get; set; }
        /// <summary>项目id</summary>
        public long contractId { get; set; }
        /// <summary>运单id</summary>
        public long waybillId { get; set; }
        /// <summary>盘次id</summary>
        public long batchId { get; set; }
        /// <summary>机组id</summary>
        public long unitId { get; set; }
        /// <summary>盘方量</summary>
        public double batchNum { get; set; }
        /// <summary>具体参数</summary>
        public IEnumerable<MaterialUsageDetailDto>? materialClassification { get; set; }
    }
    
    /// <summary>
    /// 原材料消耗明细 DTO
    /// </summary>
    public class MaterialUsageDetailDto
    {
        /// <summary>原材类型</summary>
        public string? materialType { get; set; }
        /// <summary>原材规格</summary>
        public string? materialSpecifications { get; set; }
        /// <summary>原材料供应商</summary>
        public string? materialMerchant { get; set; }
        /// <summary>原材目标消耗量</summary>
        public double? materialConsumeTargetQty { get; set; }
        /// <summary>原材实际消耗量</summary>
        public double? materialConsumeActualQty { get; set; }
        /// <summary>原材消耗时间</summary>
        public string? materialConsumeDate { get; set; }
        /// <summary>含水率</summary>
        public string? waterContent { get; set; }
        /// <summary>偏差率</summary>
        public string? percentageDeviation { get; set; }
    }
} 