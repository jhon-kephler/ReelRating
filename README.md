# 🎬 ReelRating

> 🇧🇷 Uma API moderna para avaliação de filmes e séries construída com
> .NET 8, aplicando Clean Architecture, CQRS e boas práticas de
> backend.\
> 🇺🇸 A modern movie and TV show rating API built with .NET 8, applying
> Clean Architecture, CQRS, and backend best practices.

------------------------------------------------------------------------

## ✨ Visão Geral / Overview

🇧🇷 O ReelRating é um projeto de estudo que simula uma plataforma de
avaliação de filmes e séries, onde usuários podem se autenticar, criar
reviews e avaliar conteúdos.

🇺🇸 ReelRating is a study project that simulates a movie and TV show
rating platform where users can authenticate, create reviews, and rate
content.

### 🎯 Objetivos / Goals

🇧🇷 Praticar arquitetura moderna e escalável de backend.\
🇺🇸 Practice modern and scalable backend architecture.

------------------------------------------------------------------------

## 🧩 Funcionalidades / Features

-   🔐 JWT Authentication / Autenticação JWT\
-   👤 User management / Gestão de usuários\
-   🎬 Movies & series management / Cadastro de filmes e séries\
-   ⭐ Rating system / Sistema de avaliações\
-   📝 Reviews system / Sistema de reviews\
-   📄 Pagination & filtering / Paginação e filtros\
-   🧱 Clean Architecture structure / Arquitetura limpa

------------------------------------------------------------------------

## 🧱 Arquitetura / Architecture

ReelRating ├── ReelRating.API ├── ReelRating.Application ├──
ReelRating.Domain ├── ReelRating.Infrastructure ├── ReelRatingDb └──
docker-compose.yml

------------------------------------------------------------------------

## 📊 Diagramas / Diagrams

https://miro.com/app/board/uXjVLD77ekE=/?share_link_id=145055889614

------------------------------------------------------------------------

## ⚙️ Tecnologias / Tech Stack

-   .NET 8
-   Entity Framework Core
-   MediatR
-   JWT Authentication
-   Swagger
-   Docker & Docker Compose
-   SQL Server / PostgreSQL

------------------------------------------------------------------------

## 🚀 Como Executar / How to Run

### Backend

cd ReelRatingDb docker-compose up -d

dotnet run --project ReelRating.API

------------------------------------------------------------------------

## 👨‍💻 Author

João Carlos GitHub: https://github.com/jhon-kephler

------------------------------------------------------------------------

## 📌 Note

Educational project for learning purposes.
