# Bingo API

API RESTful para jogo de Bingo desenvolvida em ASP.NET Core.

## Funcionalidades

- Cadastro de usuários
- Login com JWT
- Validação de dados com FluentValidation

## Tecnologias

- ASP.NET Core 9.0
- Entity Framework Core 9.0
- PostgreSQL
- JWT Bearer Authentication
- FluentValidation
- Swagger/OpenAPI

## Configuração

1. Configure a string de conexão no `appsettings.json` ou `DadosAcesso.json`.
2. Configure as chaves JWT.
3. Execute as migrações do EF Core: `dotnet ef database update`.

## Endpoints

### POST /api/auth/register
Registra um novo usuário.

**Request Body:**
```json
{
  "username": "string",
  "email": "string",
  "password": "string"
}
```

### POST /api/auth/login
Faz login e retorna token JWT.

**Request Body:**
```json
{
  "username": "string",
  "password": "string"
}
```

**Response:**
```json
{
  "token": "jwt_token",
  "user": {
    "id": 1,
    "username": "string",
    "email": "string",
    "createdAt": "2023-01-01T00:00:00Z",
    "lastLoginAt": "2023-01-01T00:00:00Z"
  }
}
```

## Executando

```bash
dotnet run
```

Acesse Swagger em `https://localhost:5001/swagger`.