# 📚 Book API - Sistema de Gerenciamento de Livros

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

Uma API REST robusta e escalável desenvolvida em .NET 8 para gerenciamento completo de livros, autores e gêneros literários, seguindo as melhores práticas de Clean Architecture e princípios SOLID.

## 📋 Sobre o Projeto

Este projeto foi desenvolvido como um sistema completo de gerenciamento de biblioteca, permitindo operações CRUD (Create, Read, Update, Delete) para três entidades principais: **Gêneros**, **Autores** e **Livros**. A aplicação implementa padrões modernos de desenvolvimento, incluindo CQRS com MediatR, Repository Pattern e Clean Architecture.

### 🎯 Objetivos

- ✅ Implementar um sistema completo de gerenciamento de livros
- ✅ Aplicar Clean Architecture e separação de responsabilidades
- ✅ Utilizar CQRS (Command Query Responsibility Segregation) com MediatR
- ✅ Implementar Repository Pattern com Entity Framework Core
- ✅ Criar API RESTful com versionamento
- ✅ Documentar endpoints com Swagger/OpenAPI
- ✅ Utilizar PostgreSQL como banco de dados

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, garantindo separação de responsabilidades, testabilidade e manutenibilidade:

```
📦 ApiBook
 ┣ 📂 Book.Api          # Camada de Apresentação
 ┣ 📂 Book.Application  # Camada de Aplicação
 ┣ 📂 Book.Core         # Camada de Domínio
 ┗ 📂 Book.Infra        # Camada de Infraestrutura
```

### 📐 Detalhamento das Camadas

#### **Book.Core** (Domínio)
Camada central do projeto, contendo as entidades e regras de negócio fundamentais.

- **Entidades:** Book, Author, Genre
- **Enums:** ResultType
- **Value Objects:** ValidationResult, PagedList, ErrorResponse
- **Interfaces base:** IRepositoryBase

#### **Book.Application** (Aplicação)
Contém toda a lógica de aplicação, DTOs, ViewModels e handlers.

- **DTOs:** CreateGenreDto, UpdateGenreDto, CreateAuthorDto, UpdateAuthorDto, CreateBookDto, UpdateBookDto
- **ViewModels:** GenreViewModel, AuthorViewModel, AuthorWithBooksViewModel, BookViewModel
- **Features (CQRS):**
  - Commands: CreateGenreCommand, UpdateGenreCommand, DeleteGenreCommand
  - Queries: GetPagedGenresQuery, GetGenreByIdQuery
  - Handlers: CreateGenreHandler, UpdateGenreHandler, DeleteGenreHandler, GetPagedGenresHandler, GetGenreByIdHandler
- **Profiles AutoMapper:** MappingProfiles
- **Interfaces:** IAuthorRepository, IBookRepository, IGenreRepository
- **Model Inputs:** PaginationRequest

#### **Book.Infra** (Infraestrutura)
Implementa os repositórios e configuração do banco de dados.

- **Context:** AppDbContext (Entity Framework Core)
- **Repositories:**
  - RepositoryBase (genérico)
  - AuthorRepository
  - BookRepository
  - GenreRepository
- **Migrations:** Controle de versão do banco de dados
- **Extensions:** InfrastructureServiceExtensions

#### **Book.Api** (Apresentação)
Expõe os endpoints da API e configurações.

- **Controllers:**
  - AuthorsController
  - BooksController
  - GenresController
- **Configurations:**
  - CustomControllerBase (controlador base com métodos auxiliares)
  - Service Configuration Extensions
- **Swagger/OpenAPI:** Documentação interativa da API
- **Program.cs:** Configuração da aplicação

## 🚀 Tecnologias Utilizadas

### Core
- **.NET 8.0** - Framework principal
- **C# 12.0** - Linguagem de programação
- **ASP.NET Core Web API** - Para criação da API REST

### Banco de Dados
- **PostgreSQL** - Banco de dados relacional
- **Entity Framework Core 8.0.11** - ORM
- **Npgsql.EntityFrameworkCore.PostgreSQL 8.0.11** - Provider PostgreSQL

### Bibliotecas e Padrões
- **MediatR 12.4.1** - Implementação do padrão Mediator e CQRS
- **AutoMapper 12.0.1** - Mapeamento objeto-objeto
- **Swashbuckle.AspNetCore 6.6.2** - Documentação Swagger/OpenAPI

### Padrões e Práticas Implementadas
- ✅ **CQRS** (Command Query Responsibility Segregation)
- ✅ **Repository Pattern** com implementação genérica
- ✅ **Dependency Injection** nativa do .NET
- ✅ **Clean Architecture** com separação de camadas
- ✅ **SOLID Principles**
- ✅ **DTOs e ViewModels** para transferência de dados
- ✅ **API Versionamento**
- ✅ **Padronização de respostas HTTP**
- ✅ **Paginação** de resultados
- ✅ **Validation Result Pattern**

## 📊 Modelo de Dados

### Entidades e Relacionamentos

```
┌─────────────────┐         ┌──────────────────┐         ┌─────────────────┐
│     Genre       │         │       Book       │         │     Author      │
├─────────────────┤         ├──────────────────┤         ├─────────────────┤
│ Id (PK)         │◄────────┤ Id (PK)          │├───────►│ Id (PK)         │
│ Name            │    1:N  │ Title            │  N:1    │ Name            │
│ Description     │         │ Description      │         │ Biography       │
│ CreatedAt       │         │ PublicationDate  │         │ BirthDate       │
│ UpdatedAt       │         │ ISBN             │         │ CreatedAt       │
└─────────────────┘         │ AuthorId (FK)    │         │ UpdatedAt       │
                            │ GenreId (FK)     │         └─────────────────┘
                            │ CreatedAt        │
                            │ UpdatedAt        │
                            └──────────────────┘
```

### Regras de Negócio

- ✅ Um **gênero** pode ter múltiplos livros (1:N)
- ✅ Um **autor** pode ter múltiplos livros (1:N)
- ✅ Cada **livro** pertence a apenas um autor e um gênero
- ✅ Não é possível excluir gênero/autor com livros associados
- ✅ Datas de criação e atualização são controladas automaticamente

## 📡 Endpoints da API

Todos os endpoints seguem o padrão REST e estão documentados no Swagger.

### 📗 Gêneros (`/api/genres`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/genres` | Lista todos os gêneros com paginação | 200, 400 |
| `GET` | `/api/genres/{id}` | Obtém um gênero específico por ID | 200, 404 |
| `POST` | `/api/genres` | Cria um novo gênero | 201, 400 |
| `PUT` | `/api/genres/{id}` | Atualiza um gênero existente | 200, 400, 404 |
| `DELETE` | `/api/genres/{id}` | Exclui um gênero | 204, 400, 404 |

### 👨‍💼 Autores (`/api/authors`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/authors` | Lista todos os autores com paginação | 200, 400 |
| `GET` | `/api/authors/{id}` | Obtém um autor específico por ID | 200, 404 |
| `POST` | `/api/authors` | Cria um novo autor | 201, 400 |
| `PUT` | `/api/authors/{id}` | Atualiza um autor existente | 200, 400, 404 |
| `DELETE` | `/api/authors/{id}` | Exclui um autor | 204, 400, 404 |

### 📘 Livros (`/api/books`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/books` | Lista todos os livros com paginação | 200, 400 |
| `GET` | `/api/books/{id}` | Obtém um livro específico por ID | 200, 404 |
| `POST` | `/api/books` | Cria um novo livro | 201, 400 |
| `PUT` | `/api/books/{id}` | Atualiza um livro existente | 200, 400, 404 |
| `DELETE` | `/api/books/{id}` | Exclui um livro | 204, 400, 404 |

### Paginação

Todos os endpoints GET de listagem suportam paginação através dos parâmetros:
- `page`: Número da página (padrão: 1)
- `pageSize`: Quantidade de itens por página (padrão: 20)

**Exemplo:**
```
GET /api/books?page=1&pageSize=10
```

## ⚙️ Configuração e Instalação

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- IDE (Visual Studio 2022, VS Code ou Rider)
- Git

### 🔧 Passo a Passo

1. **Clone o repositório**
```bash
git clone https://github.com/T4NCR3D3/ApiBook.git
cd ApiBook
```

2. **Configure o PostgreSQL**

Crie um banco de dados PostgreSQL ou utilize um existente.

3. **Configure a string de conexão**

Edite o arquivo `Book.Api/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=postgres;Username=seu_usuario;Password=sua_senha"
  }
}
```

> **Nota:** Para produção, configure o arquivo `appsettings.json` com as credenciais apropriadas.

4. **Restaure as dependências**
```bash
dotnet restore
```

5. **Execute as migrations do banco de dados**

Navegue até a pasta da API e execute:
```bash
cd Book.Api
dotnet ef database update --project ../Book.Infra/Book.Infra.csproj
```

Caso precise criar uma nova migration:
```bash
dotnet ef migrations add NomeDaMigration --project ../Book.Infra/Book.Infra.csproj --startup-project .
```

6. **Execute a aplicação**
```bash
dotnet run --project Book.Api
```

Ou pressione F5 no Visual Studio.

7. **Acesse a documentação Swagger**
```
https://localhost:7xxx/swagger/index.html
```

Substitua `7xxx` pela porta configurada (verifique no terminal).

## 📝 Exemplos de Requisições

### Criar um Gênero
```http
POST /api/genres
Content-Type: application/json

{
  "name": "Ficção Científica",
  "description": "Livros de ficção científica e futurismo"
}
```

**Resposta (201 Created):**
```json
{
  "id": 1,
  "name": "Ficção Científica",
  "description": "Livros de ficção científica e futurismo",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": null
}
```

### Criar um Autor
```http
POST /api/authors
Content-Type: application/json

{
  "name": "Isaac Asimov",
  "biography": "Escritor russo-americano de ficção científica",
  "birthDate": "1920-01-02"
}
```

**Resposta (201 Created):**
```json
{
  "id": 1,
  "name": "Isaac Asimov",
  "biography": "Escritor russo-americano de ficção científica",
  "birthDate": "1920-01-02T00:00:00Z",
  "createdAt": "2024-01-15T10:35:00Z",
  "updatedAt": null
}
```

### Criar um Livro
```http
POST /api/books
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

**Resposta (201 Created):**
```json
{
  "id": 1,
  "title": "Fundação",
  "description": "Primeiro livro da série Fundação",
  "publicationDate": "1951-06-01T00:00:00Z",
  "isbn": "978-0553293357",
  "createdAt": "2024-01-15T10:40:00Z",
  "updatedAt": null,
  "authorId": 1,
  "authorName": "Isaac Asimov",
  "genreId": 1,
  "genreName": "Ficção Científica"
}
```

### Obter um Livro por ID
```http
GET /api/books/1
```

**Resposta (200 OK):**
```json
{
  "id": 1,
  "title": "Fundação",
  "description": "Primeiro livro da série Fundação",
  "publicationDate": "1951-06-01T00:00:00Z",
  "isbn": "978-0553293357",
  "createdAt": "2024-01-15T10:40:00Z",
  "updatedAt": null,
  "authorId": 1,
  "authorName": "Isaac Asimov",
  "genreId": 1,
  "genreName": "Ficção Científica"
}
```

### Listar Livros com Paginação
```http
GET /api/books?page=1&pageSize=10
```

**Resposta (200 OK):**
```json
{
  "data": [
    {
      "id": 1,
      "title": "Fundação",
      "description": "Primeiro livro da série Fundação",
      "publicationDate": "1951-06-01T00:00:00Z",
      "isbn": "978-0553293357",
      "createdAt": "2024-01-15T10:40:00Z",
      "updatedAt": null,
      "authorId": 1,
      "authorName": "Isaac Asimov",
      "genreId": 1,
      "genreName": "Ficção Científica"
    }
  ],
  "currentPage": 1,
  "totalPages": 1,
  "totalItens": 1,
  "totalItemsPage": 1,
  "maxItemsPerPage": 10,
  "previousPage": null,
  "nextPage": null,
  "isLast": true,
  "paged": true
}
```

### Atualizar um Gênero
```http
PUT /api/genres/1
Content-Type: application/json

{
  "name": "Ficção Científica Clássica",
  "description": "Obras clássicas de ficção científica"
}
```

**Resposta (200 OK):**
```json
{
  "id": 1,
  "name": "Ficção Científica Clássica",
  "description": "Obras clássicas de ficção científica",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T11:00:00Z"
}
```

### Excluir um Livro
```http
DELETE /api/books/1
```

**Resposta (204 No Content):**
```
(Sem corpo de resposta)
```

## 🔒 Códigos de Status HTTP

A API utiliza os seguintes códigos de status HTTP padronizados:

| Código | Descrição |
|--------|-----------|
| `200 OK` | Requisição bem-sucedida (GET, PUT) |
| `201 Created` | Recurso criado com sucesso (POST) |
| `204 No Content` | Recurso excluído com sucesso (DELETE) |
| `400 Bad Request` | Erro de validação ou requisição inválida |
| `404 Not Found` | Recurso não encontrado |
| `500 Internal Server Error` | Erro interno do servidor |

### Estrutura de Resposta de Erro

```json
{
  "statusCode": 400,
  "message": "Mensagem de erro detalhada",
  "path": "/api/genres/999"
}
```

## 📦 Estrutura de Pastas Completa

```
📦 ApiBook
┣ 📂 Book.Api
┃ ┣ 📂 Configurations
┃ ┃ ┣ 📜 CustomControllerBase.cs
┃ ┃ ┗ 📜 ServiceExtensions.cs
┃ ┣ 📂 Controllers
┃ ┃ ┣ 📜 AuthorsController.cs
┃ ┃ ┣ 📜 BooksController.cs
┃ ┃ ┗ 📜 GenresController.cs
┃ ┣ 📜 Program.cs
┃ ┣ 📜 appsettings.json
┃ ┗ 📜 appsettings.Development.json
┣ 📂 Book.Application
┃ ┣ 📂 DTOs
┃ ┃ ┣ 📂 Author
┃ ┃ ┃ ┣ 📜 CreateAuthorDto.cs
┃ ┃ ┃ ┗ 📜 UpdateAuthorDto.cs
┃ ┃ ┣ 📂 Book
┃ ┃ ┃ ┣ 📜 CreateBookDto.cs
┃ ┃ ┃ ┗ 📜 UpdateBookDto.cs
┃ ┃ ┗ 📂 Genre
┃ ┃   ┣ 📜 CreateGenreDto.cs
┃ ┃   ┗ 📜 UpdateGenreDto.cs
┃ ┣ 📂 Features
┃ ┃ ┣ 📂 Authors
┃ ┃ ┃ ┣ 📂 Commands
┃ ┃ ┃ ┣ 📂 Queries
┃ ┃ ┃ ┗ 📂 Handlers
┃ ┃ ┣ 📂 Books
┃ ┃ ┃ ┣ 📂 Commands
┃ ┃ ┃ ┣ 📂 Queries
┃ ┃ ┃ ┗ 📂 Handlers
┃ ┃ ┗ 📂 Genres
┃ ┃   ┣ 📂 Commands
┃ ┃   ┃ ┣ 📜 CreateGenreCommand.cs
┃ ┃   ┃ ┣ 📜 UpdateGenreCommand.cs
┃ ┃   ┃ ┗ 📜 DeleteGenreCommand.cs
┃ ┃   ┣ 📂 Queries
┃ ┃   ┃ ┣ 📜 GetPagedGenresQuery.cs
┃ ┃   ┃ ┗ 📜 GetGenreByIdQuery.cs
┃ ┃   ┗ 📂 Handlers
┃ ┃     ┣ 📜 CreateGenreHandler.cs
┃ ┃     ┣ 📜 UpdateGenreHandler.cs
┃ ┃     ┣ 📜 DeleteGenreHandler.cs
┃ ┃     ┣ 📜 GetPagedGenresHandler.cs
┃ ┃     ┗ 📜 GetGenreByIdHandler.cs
┃ ┣ 📂 Interfaces
┃ ┃ ┗ 📂 Repositories
┃ ┃   ┣ 📜 IAuthorRepository.cs
┃ ┃   ┣ 📜 IBookRepository.cs
┃ ┃   ┗ 📜 IGenreRepository.cs
┃ ┣ 📂 Mappings
┃ ┃ ┗ 📜 MappingProfiles.cs
┃ ┣ 📂 ModelInputs
┃ ┃ ┗ 📜 PaginationRequest.cs
┃ ┗ 📂 ViewModels
┃   ┣ 📂 Author
┃   ┃ ┣ 📜 AuthorViewModel.cs
┃   ┃ ┗ 📜 AuthorWithBooksViewModel.cs
┃   ┣ 📂 Book
┃   ┃ ┗ 📜 BookViewModel.cs
┃   ┗ 📂 Genre
┃     ┗ 📜 GenreViewModel.cs
┣ 📂 Book.Core
┃ ┣ 📂 Common
┃ ┃ ┣ 📜 IRepositoryBase.cs
┃ ┃ ┗ 📜 PagedList.cs
┃ ┣ 📂 Entites
┃ ┃ ┣ 📜 Author.cs
┃ ┃ ┣ 📜 Book.cs
┃ ┃ ┗ 📜 Genre.cs
┃ ┣ 📂 Enum
┃ ┃ ┗ 📜 ResultType.cs
┃ ┗ 📂 ValueObjects
┃   ┣ 📜 ErrorResponse.cs
┃   ┗ 📜 ValidationResult.cs
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

## 🧪 Testes

O projeto está preparado para implementação de testes. Para adicionar testes unitários:

```bash
# Criar projeto de testes
dotnet new xunit -n Book.Tests
dotnet sln add Book.Tests/Book.Tests.csproj

# Adicionar referências aos projetos
dotnet add Book.Tests reference Book.Application/Book.Application.csproj
dotnet add Book.Tests reference Book.Core/Book.Core.csproj

# Adicionar pacotes de teste
dotnet add Book.Tests package Moq
dotnet add Book.Tests package FluentAssertions
dotnet add Book.Tests package Microsoft.EntityFrameworkCore.InMemory
```

## 🛠️ Ferramentas de Desenvolvimento

### Entity Framework Core Tools

Para trabalhar com migrations:

```bash
# Instalar ferramenta global do EF Core
dotnet tool install --global dotnet-ef

# Adicionar migration
dotnet ef migrations add NomeDaMigration --project Book.Infra --startup-project Book.Api

# Atualizar banco de dados
dotnet ef database update --project Book.Infra --startup-project Book.Api

# Remover última migration
dotnet ef migrations remove --project Book.Infra --startup-project Book.Api
```

### Swagger/OpenAPI

A documentação interativa está disponível em:
- Development: `https://localhost:{porta}/swagger`
- Endpoint JSON: `https://localhost:{porta}/swagger/v1/swagger.json`

## 📚 Recursos Adicionais

### Conceitos Aplicados

- **CQRS:** Separação entre operações de leitura (Queries) e escrita (Commands)
- **Repository Pattern:** Abstração da camada de acesso a dados
- **Mediator Pattern:** Desacoplamento entre requisições e handlers
- **DTO Pattern:** Transferência de dados entre camadas
- **Result Pattern:** Padronização de respostas com ValidationResult

### Padrões de Nomenclatura

- **Controllers:** `{Entity}Controller.cs`
- **Commands:** `{Action}{Entity}Command.cs`
- **Queries:** `Get{Entity/Entities}Query.cs`
- **Handlers:** `{Action}{Entity}Handler.cs`
- **DTOs:** `{Action}{Entity}Dto.cs`
- **ViewModels:** `{Entity}ViewModel.cs`

**Desenvolvido com ❤️ usando .NET 8 e Clean Architecture**
