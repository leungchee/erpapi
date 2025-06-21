namespace ERPAPI.Dtos
{
    /// <summary>
    /// 配合比同步 DTO
    /// </summary>
    public class ProportioningSyncDto
    {
        /// <summary>订单id</summary>
        public long OrderId { get; set; }
        /// <summary>项目id</summary>
        public long ContractId { get; set; }
        /// <summary>运单id</summary>
        public long WaybillId { get; set; }
        /// <summary>盘次id</summary>
        public long BatchId { get; set; }
        /// <summary>机组id</summary>
        public long UnitId { get; set; }
        /// <summary>盘方量</summary>
        public double BatchNum { get; set; }
        /// <summary>配比编号</summary>
        public long? ProportioningSn { get; set; }
        /// <summary>配比类型</summary>
        public long? ProportioningType { get; set; }
        /// <summary>配比体系</summary>
        public string? ProportioningSystem { get; set; }
        /// <summary>具体参数</summary>
        public IEnumerable<ProportioningMaterialDetailDto>? MaterialClassification { get; set; }

    }

    /// <summary>
    /// 配合比原材料明细 DTO
    /// </summary>
    public class ProportioningMaterialDetailDto
    {
        /// <summary>原材料类型</summary>
        public string? MaterialType { get; set; }
        /// <summary>原材料规格</summary>
        public string? MaterialSpecifications { get; set; }
        /// <summary>原材料供应商</summary>
        public string? MaterialMerchant { get; set; }
        /// <summary>理论</summary>
        public string? AdmixtureTheory { get; set; }
        /// <summary>调整</summary>
        public string? AdmixtureAdjust { get; set; }
        /// <summary>偏差率</summary>
        public string? AdmixtureDeviation { get; set; }
    }
}
