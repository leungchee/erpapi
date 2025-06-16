namespace ERPAPI.Models
{
    public class TrwdOrder
    {
        public int Fhtbh { get; set; } = 0;
        public string Fzt { get; set; } = "正在生产";
        public string Fhtdw { get; set; } = string.Empty;      // 合同单位
        public string Fgcmc { get; set; } = string.Empty;      // 工程名称
        public string Fjzbw { get; set; } = string.Empty;      // 浇筑部位
        public string Fjzfs { get; set; } = string.Empty;      // 浇筑方式
        public string Fgcdz { get; set; } = string.Empty;      // 工程地址
        public decimal Fgls { get; set; }                      // 公里数
        public string Fjhsl { get; set; } = string.Empty;      // 计划数量
        public string Fjhrq { get; set; } = string.Empty;      // 计划日期
        public string Ftpz { get; set; } = string.Empty;       // 砼品种
        public string Ftld { get; set; } = string.Empty;       // 塌落度
        public string Fjbsj { get; set; } = "0";      // 搅拌时间
        public string FRWNO { get; set; } = string.Empty;      // 任务编号
        public string FPhbNo { get; set; } = string.Empty;     // 配方编号
        public string Fscbt { get; set; } = "*";              // 生产标记
        public string FTbj { get; set; } = string.Empty;       // 备注
        public int FSgpb { get; set; }                        // 系统配方编号
        public long rwid { get; set; } = 0;                // 任务单ID

        public string? rwcode { get; set;}

    }
} 