using System;

namespace ERPAPI.Dtos
{
    /// <summary>
    /// 原材料消耗同步 DTO
    /// </summary>
    public class MaterialUsageSyncDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 原材料类型
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// 原材料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 原材料规格
        /// </summary>
        public string MaterialSku { get; set; }

        /// <summary>
        /// 原材料消耗量
        /// </summary>
        public string MaterialUsedQty { get; set; }

        /// <summary>
        /// 原材料消耗时间
        /// </summary>
        public DateTime MaterialDate { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Extension { get; set; }
    }
} 