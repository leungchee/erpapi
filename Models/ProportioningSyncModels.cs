using System.ComponentModel.DataAnnotations;

namespace ERPAPI.Models
{
    /// <summary>
    /// ERP同步配合比到智选请求模型
    /// </summary>
   

    /// <summary>
    /// ERP同步配合比到智选响应模型
    /// </summary>
    public class ProportioningSyncResponse
    {
        /// <summary>响应码</summary>
        public string Code { get; set; } = "200";
        /// <summary>提示内容</summary>
        public string Msg { get; set; } = "success";
        /// <summary>true成功 false失败</summary>
        public string Data { get; set; } = "true";
    }
} 