using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ERPAPI.Services
{
    /// <summary>
    /// Token提供者服务
    /// </summary>
    public class TokenProvider
    {
        private static string? _cachedToken;
        private static DateTime _tokenExpireTime = DateTime.MinValue;
        private static readonly object _lock = new object();
        private static readonly TimeSpan TokenValidDuration = TimeSpan.FromMinutes(55); // 设置55分钟有效期

        private readonly string _authUrl;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _httpClient;

        public TokenProvider(IConfiguration configuration, HttpClient httpClient)
        {
            _authUrl = configuration["Keycloak:AuthUrl"] ?? throw new InvalidOperationException("Keycloak:AuthUrl 配置缺失");
            _clientId = configuration["Keycloak:ClientId"] ?? throw new InvalidOperationException("Keycloak:ClientId 配置缺失");
            _clientSecret = configuration["Keycloak:ClientSecret"] ?? throw new InvalidOperationException("Keycloak:ClientSecret 配置缺失");
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// 获取访问令牌
        /// </summary>
        /// <param name="forceRefresh">是否强制刷新令牌</param>
        /// <returns>访问令牌</returns>
        public async Task<string> GetAccessTokenAsync(bool forceRefresh = false)
        {
            if (!forceRefresh && !string.IsNullOrEmpty(_cachedToken) && DateTime.Now < _tokenExpireTime)
            {
                return _cachedToken!;
            }

            if (string.IsNullOrEmpty(_authUrl) || string.IsNullOrEmpty(_clientId) || string.IsNullOrEmpty(_clientSecret))
                throw new InvalidOperationException("TokenProvider未初始化");

            var form = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "grant_type", "client_credentials" }
            };

            var content = new FormUrlEncodedContent(form);
            var response = await _httpClient.PostAsync(_authUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"获取token失败: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(result)) 
                throw new Exception("获取token响应为空");

            var obj = JObject.Parse(result);
            var token = obj["access_token"]?.ToString();
            if (string.IsNullOrWhiteSpace(token)) 
                throw new Exception("未获取到access_token");

            lock (_lock)
            {
                _cachedToken = token;
                _tokenExpireTime = DateTime.Now.Add(TokenValidDuration);
            }

            return token;
        }
    }
} 