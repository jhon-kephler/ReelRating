# 🎬 ReelRating

> API REST para avaliação de filmes e séries, construída com .NET 10 aplicando Clean Architecture, CQRS com MediatR e sincronização automática com a API do TMDB.

---

## 📐 Arquitetura

```
ReelRating/
├── ReelRating.API           → Controllers, Swagger, Program.cs
├── ReelRating.Application   → Handlers (MediatR), Services
├── ReelRating.Core          → DTOs, Requests, Responses, AutoMapper Profiles
├── ReelRating.Data          → DbContext, Repositories, Queries, Commands, Migrations
├── ReelRating.Domain        → Entidades, Interfaces de Repositório e Serviços
├── ReelRating.Infrastructure → DI, JWT Auth, HttpClient TMDB
└── ReelRating.Worker        → Background Service de sincronização com TMDB
```

Fluxo de uma requisição:
```
Controller → IMediator → Handler → Service → Query/Command → Repository → Oracle DB
```

---

## 🧩 Funcionalidades

- 🔐 Autenticação JWT (login por nickname ou e-mail)
- 👤 Cadastro de usuários com BCrypt
- 🎬 Listagem de filmes por ano, filtros e nome
- 🗂️ Filtros por categoria e ano
- 📄 Paginação server-side
- 🔄 Sincronização automática de filmes via TMDB API (Worker)
- 📖 Swagger segmentado por domínio (authentication, cine, filters)

---

## ⚙️ Tech Stack

| Tecnologia | Versão |
|---|---|
| .NET | 10 |
| Entity Framework Core | 10.0.0 |
| Oracle Database | Free (via Docker) |
| MediatR | 14.1.0 |
| AutoMapper | 16.1.1 |
| BCrypt.Net | 4.2.0 |
| JWT Bearer | 10.0.9 |
| Scrutor | 7.0.0 |
| Swashbuckle (Swagger) | 6.6.2 |

---

## 🚀 Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---

### 1. Subir o banco Oracle via Docker

```bash
docker run -d \
  --name oracle-reelrating \
  -p 1521:1521 \
  -e ORACLE_PASSWORD=ReelRatingDB2025 \
  gvenzl/oracle-free
```

Aguarde o container inicializar (~30 segundos). Verifique com:

```bash
docker logs oracle-reelrating
```

Quando aparecer `DATABASE IS READY TO USE!`, prossiga.

---

### 2. Criar o usuário/schema do banco

```bash
docker exec -it oracle-reelrating sqlplus sys/ReelRatingDB2025@FREE as sysdba
```

Dentro do SQLPlus, execute:

```sql
CREATE USER MOVIEDB IDENTIFIED BY ReelRatingDB2025;
GRANT CONNECT, RESOURCE, UNLIMITED TABLESPACE TO MOVIEDB;
EXIT;
```

---

### 3. Criar as tabelas e dados base via Migration

Na pasta `ReelRating/` (onde está o `.slnx`):

```bash
dotnet ef database update --project ReelRating.Data\ReelRating.Data.csproj --startup-project ReelRating.API\ReelRating.API.csproj
```
- `InitialCreate` — cria todas as tabelas, índices e FKs
- `SeedBaseData` — insere os dados base:
  - **Tipos:** Filme, Série
  - **Categorias de Filmes:** Ação, Aventura, Animação, Comédia, Crime, Documentário, Drama, Família, Fantasia, Histórico, Horror, Musical, Mistério, Romance, Ficção Científica, Curta-metragem, Esporte, Suspense, Guerra, Faroeste
  - **Categorias de Séries:** Sitcom, Reality Show, Talk Show, Minissérie, Novela, Policial, Teen + todos os gêneros de Filmes

---

### 4. Rodar a API

```bash
dotnet run --project ReelRating.API\ReelRating.API.csproj
```

Swagger disponível em: `https://localhost:{porta}/swagger`

---

### 5. Rodar o Worker (sincronização TMDB)

O Worker roda de forma independente da API e sincroniza filmes do TMDB automaticamente a cada 60 minutos.

```bash
dotnet run --project ReelRating.Worker\ReelRating.Worker.csproj
```

---

## 🗄️ Conectar no DBeaver

Após subir o Docker e rodar a migration, você pode inspecionar o banco:

| Campo | Valor |
|---|---|
| Driver | Oracle |
| Host | localhost |
| Port | 1521 |
| Database | FREE |
| Username | MOVIEDB |
| Password | ReelRatingDB2025 |

---

## 🔌 Endpoints

### Authentication
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| POST | `/api/Auth/SignIn` | Login (nickname ou e-mail + senha) | ❌ |
| POST | `/api/Auth/Create` | Cadastro de usuário | ❌ |

### Cine
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/Cine` | Lista filmes do ano atual (paginado) | ✅ |
| GET | `/api/Cine/Name?Name=` | Busca filme por nome | ✅ |
| GET | `/api/Cine/Filters?CategoriesId=&Year=&PageNumber=&PageSize=` | Lista por filtros | ✅ |

### Filters
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/Filters/Categories` | Lista categorias (paginado) | ❌ |
| GET | `/api/Filters/Year` | Lista anos disponíveis (paginado) | ❌ |

---

## 🔄 Worker — Sincronização TMDB

O `SyncCineJob` é um `BackgroundService` que:

1. Consulta a API do TMDB (`/discover/movie`) página por página
2. Faz upsert de filmes por `TmdbId`
3. Salva a nota do TMDB em `Notes`
4. Vincula os gêneros às categorias locais via `CineCategories`
5. Repete a cada `SyncIntervalMinutes` (padrão: 60 min)

Configuração em `ReelRating.Worker/appsettings.json`:

```json
"Tmdb": {
  "ApiKey": "sua_api_key_aqui",
  "BaseUrl": "https://api.themoviedb.org/3",
  "ImageBaseUrl": "https://image.tmdb.org/t/p/w500",
  "SyncIntervalMinutes": 60,
  "MovieTypeId": 1
}
```

Obtenha sua API Key gratuitamente em: https://www.themoviedb.org/settings/api

---

## 📊 Diagramas

Board completo com fluxos do sistema:
https://miro.com/app/board/uXjVLD77ekE=/?share_link_id=145055889614

---

## 👨‍💻 Autor

**João Carlos**
GitHub: [@jhon-kephler](https://github.com/jhon-kephler)
