# 📚 Book API - Sistema de Gerenciamento de Livros

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

Uma API REST robusta e escalável desenvolvida em .NET 8 para gerenciamento completo de livros, autores e gêneros literários, seguindo as melhores práticas de desenvolvimento de software.

## 📋 Sobre o Projeto

Este projeto foi desenvolvido como um sistema de gerenciamento de biblioteca, permitindo operações CRUD (Create, Read, Update, Delete) para três entidades principais: **Gêneros**, **Autores** e **Livros**. A aplicação foi construída seguindo os princípios SOLID, Clean Architecture e utilizando padrões modernos de desenvolvimento.

### 🎯 Objetivos

- Implementar um sistema completo de gerenciamento de livros
- Aplicar boas práticas de arquitetura de software
- Demonstrar conhecimento em .NET 8, Entity Framework Core e PostgreSQL
- Utilizar padrões de design como CQRS, Repository Pattern e Dependency Injection
- Documentar a API com Swagger/OpenAPI

## 🏗️ Arquitetura

O projeto segue uma **arquitetura em camadas** (Clean Architecture), garantindo separação de responsabilidades e alta testabilidade:

```
📦 ApiBook
 ┣ 📂 Book.Api          # Camada de Apresentação (Controllers, Configurações)
 ┣ 📂 Book.Application  # Camada de Aplicação (Use Cases, DTOs, ViewModels)
 ┣ 📂 Book.Core         # Camada de Domínio (Entidades, Interfaces)
 ┗ 📂 Book.Infra        # Camada de Infraestrutura (Repositórios, DbContext)
```

### 📐 Camadas do Projeto

#### **Book.Core** (Domínio)
- Entidades do domínio (Book, Author, Genre)
- Interfaces base (IRepositoryBase)
- Regras de negócio fundamentais

#### **Book.Application** (Aplicação)
- DTOs (Data Transfer Objects)
- ViewModels
- Commands e Queries (CQRS com MediatR)
- Handlers de negócio
- Profiles do AutoMapper
- Interfaces de repositórios específicos

#### **Book.Infra** (Infraestrutura)
- Implementação dos repositórios
- Configuração do DbContext (Entity Framework Core)
- Migrations do banco de dados
- Configurações de relacionamentos

#### **Book.Api** (Apresentação)
- Controllers RESTful com versionamento (v1)
- Configuração do Swagger
- Injeção de dependências
- Middlewares e configurações de startup

## 🚀 Tecnologias Utilizadas

### Core
- **.NET 8.0** - Framework principal
- **C# 12.0** - Linguagem de programação
- **ASP.NET Core** - Web API

### Banco de Dados
- **PostgreSQL** - Banco de dados relacional
- **Entity Framework Core 8.0.11** - ORM
- **Npgsql.EntityFrameworkCore.PostgreSQL** - Provider PostgreSQL

### Bibliotecas e Patterns
- **MediatR 12.4.1** - Padrão CQRS e Mediator
- **AutoMapper 12.0.1** - Mapeamento objeto-objeto
- **Swashbuckle.AspNetCore 6.6.2** - Documentação Swagger/OpenAPI

### Padrões e Práticas
- ✅ **CQRS** (Command Query Responsibility Segregation)
- ✅ **Repository Pattern**
- ✅ **Dependency Injection**
- ✅ **Clean Architecture**
- ✅ **SOLID Principles**
- ✅ **DTOs e ViewModels**
- ✅ **API Versionamento**
- ✅ **HTTP Status Codes padronizados**

## 📊 Modelo de Dados

### Entidades e Relacionamentos

```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│   Genre     │         │     Book     │         │   Author    │
├─────────────┤         ├──────────────┤         ├─────────────┤
│ Id          │◄───────┤│ Id           │├───────►│ Id          │
│ Name        │    1:N  │ Title        │  N:1    │ Name        │
│ Description │         │ Description  │         │ Biography   │
│ CreatedAt   │         │ PublicationDt│         │ BirthDate   │
│ UpdatedAt   │         │ ISBN         │         │ CreatedAt   │
└─────────────┘         │ AuthorId (FK)│         │ UpdatedAt   │
                        │ GenreId (FK) │         └─────────────┘
                        │ CreatedAt    │
                        │ UpdatedAt    │
                        └──────────────┘
```

### Regras de Negócio

- ✅ Um **gênero** pode ter N livros
- ✅ Um **autor** pode ter N livros
- ✅ Cada **livro** pertence a apenas um autor e um gênero
- ✅ Não é possível excluir gênero/autor com livros associados

## 📡 Endpoints da API

### 📗 Gêneros

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/v1/genres` | Lista todos os gêneros |
| `GET` | `/api/v1/genres/{id}` | Obtém um gênero por ID |
| `POST` | `/api/v1/genres` | Cria um novo gênero |
| `PUT` | `/api/v1/genres/{id}` | Atualiza um gênero existente |
| `DELETE` | `/api/v1/genres/{id}` | Exclui um gênero |

### 👨‍💼 Autores

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/v1/authors` | Lista todos os autores |
| `GET` | `/api/v1/authors/{id}` | Obtém um autor por ID |
| `POST` | `/api/v1/authors` | Cria um novo autor |
| `PUT` | `/api/v1/authors/{id}` | Atualiza um autor existente |
| `DELETE` | `/api/v1/authors/{id}` | Exclui um autor |

### 📘 Livros

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/v1/books` | Lista todos os livros |
| `GET` | `/api/v1/books/{id}` | Obtém um livro por ID |
| `POST` | `/api/v1/books` | Cria um novo livro |
| `PUT` | `/api/v1/books/{id}` | Atualiza um livro existente |
| `DELETE` | `/api/v1/books/{id}` | Exclui um livro |

## ⚙️ Configuração e Instalação

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- IDE (Visual Studio 2022, VS Code ou Rider)

### 🔧 Instalação

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/apibook.git
cd apibook
```

2. **Configure a string de conexão**

Edite o arquivo `Book.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=BookDb;Username=postgres;Password=sua_senha"
  }
}
```

3. **Restaure as dependências**
```bash
dotnet restore
```

4. **Execute as migrations**
```bash
cd Book.Api
dotnet ef migrations add InitialCreate --project ../Book.Infra/Book.Infra.csproj --startup-project .
dotnet ef database update --project ../Book.Infra/Book.Infra.csproj --startup-project .
```

5. **Execute a aplicação**
```bash
dotnet run --project Book.Api
```

6. **Acesse a documentação Swagger**
```
https://localhost:7xxx/swagger
```

## 📝 Exemplos de Uso

### Criar um Gênero
```http
POST /api/v1/genres
Content-Type: application/json

{
  "name": "Ficção Científica",
  "description": "Livros de ficção científica e futurismo"
}
```

### Criar um Autor
```http
POST /api/v1/authors
Content-Type: application/json

{
  "name": "Isaac Asimov",
  "biography": "Escritor e professor de bioquímica",
  "birthDate": "1920-01-02"
}
```

### Criar um Livro
```http
POST /api/v1/books
Content-Type: application/json

{
  "title": "Fundação",
  "description": "Primeiro livro da série Fundação",
  "publicationDate": "1951-06-01",
  "isbn": "978-0553293357",
  "authorId": 1,
  "genreId": 1
}
```

## 🧪 Testes

O projeto está preparado para receber testes unitários. Para adicionar:

```bash
dotnet new xunit -n Book.Tests
dotnet sln add Book.Tests/Book.Tests.csproj
dotnet add Book.Tests package Moq
dotnet add Book.Tests package FluentAssertions
```

## 📦 Estrutura de Pastas Detalhada

```
📦 ApiBook
┣ 📂 Book.Api
┃ ┣ 📂 Controllers
┃ ┃ ┗ 📂 V1
┃ ┃   ┣ 📜 AuthorsController.cs
┃ ┃   ┣ 📜 BooksController.cs
┃ ┃   ┗ 📜 GenresController.cs
┃ ┣ 📜 Program.cs
┃ ┣ 📜 appsettings.json
┃ ┗ 📜 appsettings.Development.json
┣ 📂 Book.Application
┃ ┣ 📂 Common
┃ ┃ ┗ 📜 Result.cs
┃ ┣ 📂 DTOs
┃ ┃ ┣ 📂 Author (CreateAuthorDto, UpdateAuthorDto)
┃ ┃ ┣ 📂 Book (CreateBookDto, UpdateBookDto)
┃ ┃ ┗ 📂 Genre (CreateGenreDto, UpdateGenreDto)
┃ ┣ 📂 Features
┃ ┃ ┣ 📂 Authors (Commands, Queries, Handlers)
┃ ┃ ┣ 📂 Books (Commands, Queries, Handlers)
┃ ┃ ┗ 📂 Genres (Commands, Queries, Handlers)
┃ ┣ 📂 Interfaces
┃ ┃ ┗ 📂 Repositories
┃ ┣ 📂 Mappings (AutoMapper Profiles)
┃ ┗ 📂 ViewModels
┣ 📂 Book.Core
┃ ┗ 📂 Entites
┃   ┣ 📜 Author.cs
┃   ┣ 📜 Book.cs
┃   ┗ 📜 Genre.cs
┣ 📂 Book.Infra
┃ ┣ 📂 Context
┃ ┃ ┗ 📜 AppDbContext.cs
┃ ┣ 📂 Extensions
┃ ┃ ┗ 📜 InfrastructureServiceExtensions.cs
┃ ┣ 📂 Migrations
┃ ┗ 📂 Repositories
┃   ┣ 📂 Base
┃   ┃ ┗ 📜 RepositoryBase.cs
┃   ┣ 📜 AuthorRepository.cs
┃   ┣ 📜 BookRepository.cs
┃   ┗ 📜 GenreRepository.cs
┗ 📜 README.md
```

## 🔒 HTTP Status Codes

A API utiliza os seguintes códigos de status HTTP:

- `200 OK` - Requisição bem-sucedida
- `201 Created` - Recurso criado com sucesso
- `400 Bad Request` - Erro de validação ou requisição inválida
- `404 Not Found` - Recurso não encontrado
- `500 Internal Server Error` - Erro interno do servidor

**Desenvolvido com ❤️ usando .NET 8**
