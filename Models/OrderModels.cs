using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ERPAPI.Models
{
    /// <summary>
    /// 智选推送订单请求模型
    /// </summary>
    public class OrderPushRequest
    {
        /// <summary>
        /// 订单id，唯一标识
        /// </summary>
        [JsonPropertyName("orderId")]
        [Required(ErrorMessage = "订单ID不能为空")]
        public long OrderId { get; set; }

        /// <summary>
        /// 订单编号，唯一，用于展示
        /// </summary>
        [JsonPropertyName("orderSn")]
        [Required(ErrorMessage = "订单编号不能为空")]
        public string OrderSn { get; set; } = string.Empty;

        /// <summary>
        /// 订单来源(10智选)
        /// </summary>
        [JsonPropertyName("sourceType")]
        [Required(ErrorMessage = "订单来源不能为空")]
        [RegularExpression("^10$", ErrorMessage = "订单来源必须为10")]
        public string SourceType { get; set; } = "10";

        /// <summary>
        /// 订单类型(10商砼20砂浆)
        /// </summary>
        [JsonPropertyName("orderType")]
        [Required(ErrorMessage = "订单类型不能为空")]
        [RegularExpression("^(10|20)$", ErrorMessage = "订单类型必须为10或20")]
        public string OrderType { get; set; } = "10";

        /// <summary>
        /// 规格型号(如C10、C30)
        /// </summary>
        [JsonPropertyName("productName")]
        [Required(ErrorMessage = "规格型号不能为空")]
        [DefaultValue("C10")]
        public string ProductName { get; set; } = "C10";

        /// <summary>
        /// 二级分类名称
        /// </summary>
        [JsonPropertyName("secondCategoryName")]
        [Required(ErrorMessage = "二级分类名称不能为空")]
        public string SecondCategoryName { get; set; } = string.Empty;

        /// <summary>
        /// ERP合同id
        /// </summary>
        [JsonPropertyName("erpId")]
        [Required(ErrorMessage = "ERP合同ID不能为空")]
        public string ErpId { get; set; } = string.Empty;

        /// <summary>
        /// 订单方量
        /// </summary>
        [JsonPropertyName("orderQuantity")]
        [Required(ErrorMessage = "订单方量不能为空")]
        [DefaultValue("100")]
        public string OrderQuantity { get; set; } = "100";

        /// <summary>
        /// 抗渗等级
        /// </summary>
        [JsonPropertyName("impermeabilityGrade")]
        public string? ImpermeabilityGrade { get; set; }

        /// <summary>
        /// 抗冻等级
        /// </summary>
        [JsonPropertyName("antifreezeGrade")]
        public string? AntifreezeGrade { get; set; }

        /// <summary>
        /// 抗折等级
        /// </summary>
        [JsonPropertyName("bendingGrade")]
        public string? BendingGrade { get; set; }

        /// <summary>
        /// 特殊类型用，分割
        /// </summary>
        [JsonPropertyName("admixtureCombination")]
        public string? AdmixtureCombination { get; set; }

        /// <summary>
        /// 塌落度
        /// </summary>
        [JsonPropertyName("slumps")]
        public string? Slumps { get; set; }

        /// <summary>
        /// 浇筑方式
        /// </summary>
        [JsonPropertyName("pouringType")]
        [Required(ErrorMessage = "浇筑方式不能为空")]
        [DefaultValue("泵送")]
        public string PouringType { get; set; } = "泵送";

        /// <summary>
        /// 浇筑部位
        /// </summary>
        [JsonPropertyName("location")]
        [Required(ErrorMessage = "浇筑部位不能为空")]
        [DefaultValue("AAAA")]
        public string Location { get; set; } = "AAAA";

        /// <summary>
        /// 订单备注
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }

        /// <summary>
        /// 送达时间(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [JsonPropertyName("deliveryTime")]
        [Required(ErrorMessage = "送达时间不能为空")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "送达时间格式必须为 yyyy-MM-dd HH:mm:ss")]
        public string DeliveryTime { get; set; } = string.Empty;

        /// <summary>
        /// 厂站id(erp系统中的厂站id)
        /// </summary>
        [JsonPropertyName("stationId")]
        [Required(ErrorMessage = "厂站ID不能为空")]
        public string StationId { get; set; } = string.Empty;

        /// <summary>
        /// 厂站名称
        /// </summary>
        [JsonPropertyName("station")]
        [Required(ErrorMessage = "厂站名称不能为空")]
        public string Station { get; set; } = string.Empty;

        /// <summary>
        /// 合同id
        /// </summary>
        [JsonPropertyName("contractId")]
        public long? ContractId { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        [JsonPropertyName("contractName")]
        public string? ContractName { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// 地址经度
        /// </summary>
        [JsonPropertyName("addressLongitude")]
        public string? AddressLongitude { get; set; }

        /// <summary>
        /// 地址纬度
        /// </summary>
        [JsonPropertyName("addressLatitude")]
        public string? AddressLatitude { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        [JsonPropertyName("placer")]
        [Required(ErrorMessage = "收货人不能为空")]
        public string Placer { get; set; } = string.Empty;

        /// <summary>
        /// 下单时间
        /// </summary>
        [JsonPropertyName("orderTime")]
        [Required(ErrorMessage = "下单时间不能为空")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "下单时间格式必须为 yyyy-MM-dd HH:mm:ss")]
        public string OrderTime { get; set; } = string.Empty;

        /// <summary>
        /// 收货人电话
        /// </summary>
        [JsonPropertyName("placerPhone")]
        [Required(ErrorMessage = "收货人电话不能为空")]
        [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        public string PlacerPhone { get; set; } = string.Empty;

        /// <summary>
        /// 客户名称(甲方名称)
        /// </summary>
        [JsonPropertyName("companyName")]
        public string? CompanyName { get; set; }

        /// <summary>
        /// 工程名称(工地名称)
        /// </summary>
        [JsonPropertyName("siteName")]
        public string? SiteName { get; set; }

        /// <summary>
        /// 拓展字段，方便后续进行接口拓展!数据为序列化后的json字符串
        /// </summary>
        [JsonPropertyName("extra")]
        public string? Extra { get; set; }
    }

    public class ApiResponse<T>
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = "200";

        [JsonPropertyName("msg")]
        public string Message { get; set; } = "success";

        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
} 