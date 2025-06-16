# ERP API

ERP API for DataSync Application

## 功能特性

- JWT 认证
- 物流信息同步
- 原材料消耗信息同步
- Keycloak 认证集成

## API 端点

### 认证相关

#### 1. 内部登录 API
```
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

#### 2. Keycloak 认证端点
```
POST /auth/realms/Ct/protocol/openid-connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials&client_id=your-client-id&client_secret=your-client-secret&scope=openid
```

**响应示例：**
```json
{
  "access_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9...",
  "token_type": "Bearer",
  "expires_in": 3300,
  "scope": "openid"
}
```

### 数据同步

#### 物流信息同步
```
POST /openApi/thirdErpApi/v2/erpTicket
Authorization: Bearer {token}
Content-Type: application/json
```

#### 原材料消耗信息同步
```
POST /api/MaterialUsageSync/sync
Authorization: Bearer {token}
Content-Type: application/json
```

## 配置

在 `appsettings.json` 中配置以下参数：

```json
{
  "Keycloak": {
    "AuthUrl": "http://localhost:8080/auth/realms/Ct/protocol/openid-connect/token",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret"
  }
}
```

## 运行

```bash
dotnet run
```

访问 Swagger UI: http://localhost:5266/swagger