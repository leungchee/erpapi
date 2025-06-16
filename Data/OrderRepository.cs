using Dapper;
using ERPAPI.Models;
using System.Collections.Generic;
using System.Data;
using static System.Net.WebRequestMethods;

namespace ERPAPI.Data
{
    public interface IOrderRepository
    {
        Task<string> CreateOrderAsync(OrderPushRequest request);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(IDbConnection connection, ILogger<OrderRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task<string> CreateOrderAsync(OrderPushRequest request)
        {
            try
            {
                // 将 OrderPushRequest 转换为 TrwdOrder
                var trwdOrder = MapToTrwdOrder(request);

                // 生成任务编号
                trwdOrder.FRWNO = GenerateTaskCode();

                const string sql = @"
                    INSERT INTO trwd (
                        fhtbh, fzt, fhtdw, fgcmc, fjzbw, fjzfs, fgcdz, fgls, 
                        fjhrq, ftpz, ftld, fjhsl, fjbsj, FRWNO, FPhbNo, fscbt, 
                        FTbj, FSgpb,rwid, rwcode
                    ) VALUES (
                        @Fhtbh, @Fzt, @Fhtdw, @Fgcmc, @Fjzbw, @Fjzfs, @Fgcdz, @Fgls,
                        @Fjhrq, @Ftpz, @Ftld, @Fjhsl, @Fjbsj, @FRWNO, @FPhbNo, @Fscbt,
                        @FTbj, @FSgpb, @rwid, @rwcode
                    );
                    SELECT SCOPE_IDENTITY();";

                var orderId = await _connection.ExecuteScalarAsync<int>(sql, trwdOrder);
                return trwdOrder.FRWNO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "创建订单失败: {Message}", ex.Message);
                throw;
            }
        }
        private TrwdOrder MapToTrwdOrder(OrderPushRequest request)
        {

            //insert into trwd(fhtbh, fzt, fhtdw, fgcmc, fjzbw, fjzfs, fgcdz, fgls, fjhrq, ftpz, ftld, fjhsl, fjbsj, FRWNO, FPhbNo, fscbt, FTbj, FSgpb)
            //                        values(0, '正在生产', '{strBuilder}', '{strProjName}', '{strConsPos}', '{strCatingMode}', '{strAddress}',{ decLoadDistance},'{strPlannedtime}','{strStrengthLevel}'
            //                        ,'{strSlumps}',{ strStere},{ strMixerTime},'{strTaskCode}','{strFormulaCode}','*',N'{strRWDRemaks}',{ strConSysFormulaCode}); ";这个是数据库的插入语句。

            return new TrwdOrder
            {
                Fhtbh = 0,
                Fzt = "正在生产",
                Fhtdw = request.CompanyName ?? string.Empty,        // 合同单位
                Fgcmc = request.SiteName ?? string.Empty,          // 工程名称
                Fjzbw = request.Location,                          // 浇筑部位
                Fjzfs = request.PouringType,                       // 浇筑方式
                Fgcdz = request.Address ?? string.Empty,           // 工程地址
                Fgls = 0,                                          // 公里数，需要根据地址计算
                Fjhrq = request.DeliveryTime,                      // 计划日期
                Ftpz = request.ProductName,                        // 砼品种
                Ftld = request.Slumps ?? string.Empty,             // 塌落度
                Fjhsl = request.OrderQuantity,                     // 计划数量
                FRWNO = request.OrderSn,                              // 任务编号，将在插入时生成
                FPhbNo = request.ErpId,                           // 配方编号
                Fscbt = "*",                                      // 生产标记
                FTbj = request.Remark ?? string.Empty,            // 备注
                FSgpb = 0  ,                                       // 系统配方编号
                rwid=request.OrderId, // 任务单ID
                rwcode=request.OrderSn,
            };
        }

        private string GenerateTaskCode()
        {
            // 生成任务编号：年月日时分秒+4位随机数
            return $"{DateTime.Now:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        }
    }
} 