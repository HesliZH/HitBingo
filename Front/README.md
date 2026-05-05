# 🎲 BINGO Frontend

Frontend Angular para o jogo de Bingo online.

## 📋 Pré-requisitos

- Node.js (v18.x ou superior)
- npm ou yarn
- Angular CLI (v18.x)

## 🚀 Instalação

1. Instale as dependências:
```bash
npm install
```

2. Inicie o servidor de desenvolvimento:
```bash
npm start
```

3. Abra o navegador e acesse: `http://localhost:4200`

## 📁 Estrutura do Projeto

```
src/
├── app/
│   ├── core/                 # Serviços singleton e interceptors
│   │   ├── services/
│   │   │   └── auth.service.ts
│   │   └── interceptors/
│   │       └── auth.interceptor.ts
│   ├── guards/               # Guardas de rota
│   │   └── auth.guard.ts
│   ├── layout/               # Componentes estruturais
│   │   └── components/
│   │       └── navbar/
│   ├── modules/              # Feature modules
│   │   ├── home/
│   │   ├── auth/
│   │   └── game/
│   ├── shared/               # Componentes reutilizáveis
│   ├── app.component.*
│   ├── app.module.ts
│   └── app-routing.module.ts
├── assets/                   # Imagens, ícones, fontes
├── environments/             # Configurações de ambiente
├── styles.scss               # Estilos globais
├── variables.scss            # Variáveis SCSS
└── theme-light.scss          # Temas
```

## 🎨 Paleta de Cores

- **Primário:** #641f5e
- **Secundário:** #686077
- **Terciário:** #66ac92
- **Quaternário:** #c2bf92
- **Quintário:** #edd58f

## 🔐 Autenticação

O serviço de autenticação está em `src/app/core/services/auth.service.ts` e gerencia:
- Login e cadastro
- Armazenamento de token JWT
- Requisições autenticadas (via interceptor)
- Guarda de rotas protegidas

## 📱 Páginas Disponíveis

- **Home** (`/home`) - Página inicial com informações sobre o jogo
- **Login** (`/auth/login`) - Página de autenticação
- **Cadastro** (`/auth/register`) - Página de criação de conta
- **Jogo** (`/game`) - Página do jogo (protegida por autenticação)

## ⚙️ Configuração da API

Atualize a URL da API em `src/app/core/services/auth.service.ts`:

```typescript
private apiUrl = 'http://localhost:5000/api/auth';
```

## 🎯 Próximas Etapas

- [ ] Implementar funcionalidades do jogo
- [ ] Adicionar suporte para WebSocket (tempo real)
- [ ] Criar página de salas de jogo
- [ ] Adicionar animações e efeitos visuais
- [ ] Implementar temas claro/escuro
- [ ] Adicionar testes unitários e e2e

## 📝 Build para Produção

```bash
npm run build
```

Os arquivos compilados estarão em `dist/bingo-front/`

## 🛠️ Desenvolvimento

Para mode de desenvolvimento com hot reload:

```bash
npm start
```

## 📄 Licença

Projeto privado - Todos os direitos reservados.
