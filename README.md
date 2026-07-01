# 🎬 ReelRating

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4?logo=dotnet)
![Oracle](https://img.shields.io/badge/Oracle-Free-F80000?logo=oracle)
![MediatR](https://img.shields.io/badge/MediatR-14.1-blue)
![Tests](https://img.shields.io/badge/tests-122%20passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-green)

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
└── ReelRating.Worker        → Background Services (TMDB Sync + Customer Notes)
```

Fluxo de uma requisição:
```
Controller → IMediator → Handler → Service → Query/Command → Repository → Oracle DB
```

---

## 🧩 Funcionalidades

- 🔐 Autenticação JWT (login por nickname ou e-mail + BCrypt)
- 👤 Cadastro de usuários com validação de duplicidade
- 🎬 Listagem de filmes por ano, filtros e nome
- 🗂️ Filtros por categoria e ano com paginação server-side
- ⭐ Sistema de reviews (criar, editar, soft-delete, listar por usuário)
- 💬 Sistema de comentários (criar, soft-delete, listar)
- ❤️ Favoritos (adicionar, remover, listar por usuário)
- 📝 Notas do TMDB e médias de avaliação dos usuários
- 🔄 Sincronização automática de filmes via TMDB API
- 📊 Cálculo automático de média de notas por filme
- 📖 Swagger segmentado por domínio com autenticação integrada

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
| xUnit | 2.9.3 |
| Moq | 4.20.72 |

---

## 🚀 Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---

### 1. Subir o banco Oracle via Docker

```bash
docker run -d --name oracle-reelrating -p 1521:1521 -e ORACLE_PASSWORD=ReelRatingDB2025 gvenzl/oracle-free
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

Esse comando executa as migrations em ordem:
- `InitialCreate` — cria todas as tabelas, índices e FKs
- `SeedBaseData` — insere os dados base:
  - **Tipos:** Filme, Série
  - **Categorias de Filmes:** Ação, Aventura, Animação, Comédia, Crime, Documentário, Drama, Família, Fantasia, Histórico, Horror, Musical, Mistério, Romance, Ficção Científica, Curta-metragem, Esporte, Suspense, Guerra, Faroeste
  - **Categorias de Séries:** Sitcom, Reality Show, Talk Show, Minissérie, Novela, Policial, Teen + todos os gêneros de Filmes
- `RemoveReviewCategoriesAndStatus` — ajuste no modelo de Review

---

### 4. Rodar a API

```bash
dotnet run --project ReelRating.API\ReelRating.API.csproj
```

Swagger disponível em: `https://localhost:{porta}/swagger`

---

### 5. Rodar o Worker

O Worker roda de forma independente da API e executa dois jobs automáticos:

```bash
dotnet run --project ReelRating.Worker\ReelRating.Worker.csproj
```

---

## 🗄️ Conectar no DBeaver

Após subir o Docker e rodar as migrations, você pode inspecionar o banco:

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
| GET | `/api/Cine/Filters?CategoriesId=&Year=&PageNumber=&PageSize=` | Lista com filtros | ✅ |

### Filters
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/Filters/Categories` | Lista categorias (paginado) | ❌ |
| GET | `/api/Filters/Year` | Lista anos disponíveis (paginado) | ❌ |

### Review
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/Review?Id=` | Busca review por id | ✅ |
| GET | `/api/Review/List?Id=&PageNumber=&PageSize=` | Lista reviews do usuário | ✅ |
| POST | `/api/Review/Create` | Cria review | ✅ |
| POST | `/api/Review/Update` | Atualiza review | ✅ |
| POST | `/api/Review/Delete` | Soft-delete de review | ✅ |

### Comments
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/Comments?Id=&CustomerId=` | Busca comentário por id e usuário | ✅ |
| GET | `/api/Comments/Cine?Id=&CustomerId=&CineId=` | Busca comentário por filme | ✅ |
| GET | `/api/Comments/ListById?Id=&PageNumber=&PageSize=` | Lista comentários do usuário | ✅ |
| GET | `/api/Comments/List?PageNumber=&PageSize=` | Lista todos os comentários | ✅ |
| POST | `/api/Comments/Create` | Cria comentário | ✅ |
| POST | `/api/Comments/Delete` | Soft-delete de comentário | ✅ |

### Favorites
| Método | Rota | Descrição | Auth |
|---|---|---|---|
| GET | `/api/Favorites?Id=&CustomerId=` | Busca favorito por id e usuário | ✅ |
| GET | `/api/Favorites/Cine?Id=&CustomerId=&CineId=` | Busca favorito por filme | ✅ |
| GET | `/api/Favorites/ListById?Id=&PageNumber=&PageSize=` | Lista favoritos do usuário | ✅ |
| GET | `/api/Favorites/List?PageNumber=&PageSize=` | Lista todos os favoritos | ✅ |
| POST | `/api/Favorites/Create` | Adiciona favorito (upsert) | ✅ |
| POST | `/api/Favorites/Delete` | Remove favorito (soft-delete) | ✅ |

---

## 🔄 Worker — Jobs Automáticos

### SyncCineJob
Roda a cada 60 minutos e sincroniza filmes do TMDB:
1. Consulta a API do TMDB (`/discover/movie`) página por página
2. Faz upsert de filmes por `TmdbId`
3. Salva a nota do TMDB em `Notes`
4. Vincula os gêneros às categorias locais via `CineCategories`

### CustomerNotesJob
Roda a cada 10 minutos e recalcula a média de notas dos usuários:
1. Agrupa as reviews por filme (excluindo soft-deleted)
2. Calcula a média das notas
3. Atualiza `Notes.CustomerNotes` para cada filme avaliado

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

## 🧪 Testes

```bash
dotnet test ReelRating.Tests\ReelRating.Tests.csproj
```

**116 testes unitários** cobrindo Handlers e Services de todos os domínios:

| Domínio | Handlers | Services |
|---|---|---|
| Auth | ✅ | ✅ |
| Cine | ✅ | ✅ |
| Reviews | ✅ | ✅ |
| Comments | ✅ | ✅ |
| Favorites | ✅ | ✅ |
| Filters | ✅ | ✅ |

---

## 🗺️ Roadmap

- [x] Autenticação JWT com BCrypt
- [x] Sistema de reviews com soft-delete
- [x] Sistema de comentários
- [x] Favoritos por usuário
- [x] Sincronização automática via TMDB
- [x] Cálculo automático de média de notas
- [x] Testes unitários (122 testes)
- [ ] Frontend web
- [ ] Histórico de filmes assistidos
- [ ] Recomendações baseadas em preferências
- [ ] Suporte a séries com episódios
- [ ] Notificações de novos lançamentos

---

## 📊 Diagramas

Board completo com fluxos do sistema:
https://miro.com/app/board/uXjVLD77ekE=/?share_link_id=145055889614

---

## 👨‍💻 Autor

**João Carlos**
GitHub: [@jhon-kephler](https://github.com/jhon-kephler)

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
