using ERPAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ERPAPI.Controllers
{
    /// <summary>
    /// Keycloak 认证控制器
    /// </summary>
    [ApiController]
    [Route("auth/realms/Ct/protocol/openid-connect")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class KeycloakAuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public KeycloakAuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        /// <summary>
        /// 获取访问令牌
        /// </summary>
        /// <param name="request">令牌请求参数</param>
        /// <returns>访问令牌响应</returns>
        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> GetToken([FromForm] TokenRequest request)
        {
            try
            {
                // 验证请求参数
                if (request == null)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Error = "invalid_request",
                        ErrorDescription = "请求参数不能为空"
                    });
                }

                // 验证 grant_type
                //if (string.IsNullOrEmpty(request.GrantType))
                //{
                //    return BadRequest(new ErrorResponse
                //    {
                //        Error = "invalid_request",
                //        ErrorDescription = "grant_type 参数不能为空"
                //    });
                //}

                // 目前只支持 client_credentials 授权类型
                //if (request.GrantType != "client_credentials")
                //{
                //    return BadRequest(new ErrorResponse
                //    {
                //        Error = "unsupported_grant_type",
                //        ErrorDescription = "不支持的授权类型"
                //    });
                //}

                // 获取访问令牌

                var token = _jwtService.GenerateToken("admin");
              
                return Ok(new TokenResponse
                {
                    access_token = token,
                    TokenType = "Bearer",
                    ExpiresIn = 33000, // 55分钟 = 3300秒
                    Scope = "openid"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "invalid_request",
                    ErrorDescription = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "server_error",
                    ErrorDescription = $"获取令牌时发生错误: {ex.Message}"
                });
            }
        }
    }

    /// <summary>
    /// 令牌请求模型
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// 授权类型
        /// </summary>
        public string? GrantType { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        /// 客户端密钥
        /// </summary>
        public string? ClientSecret { get; set; }

        /// <summary>
        /// 作用域
        /// </summary>
        public string? Scope { get; set; }
    }

    /// <summary>
    /// 令牌响应模型
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string access_token { get; set; } = string.Empty;

        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// 过期时间（秒）
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 作用域
        /// </summary>
        public string? Scope { get; set; }
    }

    /// <summary>
    /// 错误响应模型
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorDescription { get; set; } = string.Empty;
    }
} 