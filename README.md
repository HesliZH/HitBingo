# Bingo Open Source

Bem-vindo ao projeto **Bingo** — uma aplicação Open Source desenvolvida em ASP.NET Core para demonstrar um sistema de jogo de bingo moderno, com API, persistência em banco de dados e funcionalidades de sala/cartela.

> Este projeto faz parte do portfólio e está em constante evolução. Novas seções serão acrescentadas ao README conforme o desenvolvimento avança.

---

## ✨ Visão Geral

O objetivo deste projeto é construir uma API de bingo organizada, segura e preparada para uso em aplicações web e mobile.

### Principais funcionalidades

- Criação de salas de bingo com UUID único
- Geração de cartelas de bingo por jogador
- Persistência de cartelas em banco de dados
- Sorteio de números de `1` a `75` sem repetição por sala
- Consulta dos números já sorteados em cada sala
- Autenticação de usuário com JWT
- Validação de dados com FluentValidation
- Documentação via Swagger / OpenAPI

---

## 🧱 Estrutura do projeto

- `bingo.sln` — solução principal
- `API/` — backend RESTful em ASP.NET Core
- `Front/` — front-end do projeto (se houver implementado)
- `estrutura.md` — estrutura e notas do projeto

---

## 🚀 Tecnologias usadas

- ASP.NET Core 9.0
- Entity Framework Core 9.0
- PostgreSQL
- JWT Bearer Authentication
- FluentValidation
- Swagger/OpenAPI

---

## 🧩 Como rodar o projeto

1. Clone o repositório

```bash
git clone <repo-url>
cd bingo
```

2. Configure o banco de dados

- Edite `API/appsettings.json` ou `API/DadosAcesso.json`
- Ajuste `DefaultConnection` para o seu PostgreSQL
- Configure `Jwt:Issuer`, `Jwt:Audience` e `Jwt:Key`

3. Execute as migrações

```bash
cd API
dotnet ef database update
```

4. Inicie a API

```bash
dotnet run
```

5. Acesse a documentação do Swagger

```text
https://localhost:5001/swagger
```

---

## 📌 Endpoints principais da API

### Autenticação

- `POST /api/auth/register` — registra um usuário
- `POST /api/auth/login` — obtém token JWT

### Bingo

- `GET /api/bingo/cartela` — gera uma nova cartela de bingo
- `POST /api/bingo/cartela` — cria e salva uma cartela associada a uma sala e jogador
- `POST /api/bingo/sorteia` — sorteia um número não repetido em uma sala
- `GET /api/bingo/sorteados` — retorna números sorteados da sala

---

## 🗂️ Banco de dados

O projeto possui tabelas importantes para o funcionamento do bingo:

- `Users` — cadastro de usuário
- `SALAS` — salas de jogo com UUID único
- `SALAS_CARTELAS` — cartelas associadas a salas e jogadores
- `DrawnNumbers` — números já sorteados por sala

---

## 💡 Para o portfólio

Esse projeto é ideal para mostrar:

- design de API RESTful
- modelagem de jogo em tempo real
- persistência de estado de sala/cartela
- automação de geração de números aleatórios
- uso de autenticação JWT

---

## 🌱 Próximos passos

Vou continuar incrementando o README com:

- instruções de teste
- exemplos de requisições completas
- roteiro de desenvolvimento
- descrição do front-end
- deploy e CI/CD

---

## 🤝 Contribuições

Contribuições são bem-vindas! Caso queira melhorar o projeto, basta abrir uma issue ou enviar um pull request.

---

## 📫 Contato

Se quiser falar sobre o projeto ou portfólio, deixe uma nota no `README` ou direcione perguntas diretamente no repositório.
