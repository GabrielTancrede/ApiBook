# 📚 Book API - Sistema de Gerenciamento de Livros

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-8.0-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![xUnit](https://img.shields.io/badge/xUnit-50_tests-brightgreen?style=for-the-badge&logo=dotnet&logoColor=white)

Uma API REST robusta e escalável desenvolvida em .NET 8 para gerenciamento completo de livros, autores e gêneros literários, seguindo as melhores práticas de Clean Architecture e princípios SOLID.

## 📋 Sobre o Projeto

Este projeto foi desenvolvido como um sistema completo de gerenciamento de biblioteca, permitindo operações CRUD (Create, Read, Update, Delete) para três entidades principais: **Gêneros**, **Autores** e **Livros**. A aplicação implementa padrões modernos de desenvolvimento, incluindo CQRS com MediatR, Repository Pattern e Clean Architecture.

### 🎯 Objetivos

- ✅ Implementar um sistema completo de gerenciamento de livros
- ✅ Aplicar Clean Architecture e separação de responsabilidades
- ✅ Utilizar CQRS (Command Query Responsibility Segregation) com MediatR
- ✅ Implementar Repository Pattern com Entity Framework Core
- ✅ Criar API RESTful com versionamento (`/api/v1/`)
- ✅ Documentar endpoints com Swagger/OpenAPI
- ✅ Utilizar PostgreSQL como banco de dados
- ✅ Validação de entrada com **DataAnnotations** em todos os DTOs
- ✅ Validação de duplicidade (gêneros, autores e livros)
- ✅ Testes unitários abrangentes com **xUnit**, **Moq** e **FluentAssertions** (50 testes)

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
- ✅ **Input Validation** com DataAnnotations nos DTOs
- ✅ **Duplicate Validation** (verificação de duplicidade no banco)
- ✅ **Unit Testing** com xUnit + Moq + FluentAssertions

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
- ✅ **Nomes de gêneros** devem ser únicos (case-insensitive)
- ✅ **Nomes de autores** devem ser únicos (case-insensitive)
- ✅ **Livros** não podem ter título duplicado para o mesmo autor (case-insensitive)
- ✅ Validação de entrada obrigatória em todos os DTOs (campos obrigatórios, tamanho máximo, etc.)
- ✅ Validação de **formato ISBN-10/ISBN-13** no campo ISBN dos livros (com ou sem hífens)

## � Validação e Segurança

### Validação de Entrada (DataAnnotations)

Todos os DTOs possuem validação com **DataAnnotations**, garantindo que os dados enviados à API estejam corretos antes de atingir o handler:

| DTO | Campos Obrigatórios | Regras |
|-----|---------------------|--------|
| CreateGenreDto / UpdateGenreDto | Name | Name: max 100 chars; Description: max 500 chars |
| CreateAuthorDto / UpdateAuthorDto | Name | Name: max 150 chars; Biography: max 2000 chars |
| CreateBookDto / UpdateBookDto | Title, AuthorId, GenreId | Title: max 200 chars; Description: max 2000 chars; ISBN: formato ISBN-10 ou ISBN-13 (hífens opcionais) |

A API utiliza `InvalidModelStateResponseFactory` para retornar automaticamente respostas `400 Bad Request` padronizadas quando os dados falham na validação.

#### 📖 Validação de ISBN

O campo `ISBN` dos DTOs de livro aceita **ISBN-10** e **ISBN-13**, com ou sem hífens separadores, validado via `[RegularExpression]`:

| Formato | Exemplo válido | Regra |
|---------|---------------|-------|
| **ISBN-10** | `0-306-40615-2` ou `0306406152` | 9 dígitos + dígito/`X` verificador |
| **ISBN-13** | `978-65-5939-484-6` ou `9786559394846` | 12 dígitos + dígito verificador |

Regex utilizada:
```
^(?:\d[-]?){9}[\dX]$|^(?:\d[-]?){12}\d$
```

Exemplos de valores **inválidos** (resultam em `400 Bad Request`):
- Letras ou caracteres especiais além de `-` e `X` final
- Sequência com número incorreto de dígitos (ex: `12345`)
- ISBN-10 com `X` em posição diferente da última

### Validação de Duplicidade

A camada de aplicação verifica duplicidade antes de criar ou atualizar registros:

| Entidade | Regra de Duplicidade |
|----------|---------------------|
| Gênero | Nome único (case-insensitive) |
| Autor | Nome único (case-insensitive) |
| Livro | Título único por autor (case-insensitive) |

> Na atualização, o próprio registro é excluído da verificação (`excludeId`) para permitir salvar sem alterar o nome/título.

### Boas Práticas de Segurança

- ✅ Senha do banco de dados configurada via placeholder `${DB_PASSWORD}` no `appsettings.json`
- ✅ Credenciais reais apenas no `appsettings.Development.json` (excluído do Git)
- ✅ Pacote `Microsoft.EntityFrameworkCore.SqlServer` removido (projeto utiliza apenas PostgreSQL)

## �📡 Endpoints da API

Todos os endpoints seguem o padrão REST e estão documentados no Swagger.

### 📗 Gêneros (`/api/v1/genres`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/v1/genres` | Lista todos os gêneros com paginação | 200, 400 |
| `GET` | `/api/v1/genres/{id}` | Obtém um gênero específico por ID | 200, 404 |
| `POST` | `/api/v1/genres` | Cria um novo gênero | 201, 400 |
| `PUT` | `/api/v1/genres/{id}` | Atualiza um gênero existente | 200, 400, 404 |
| `DELETE` | `/api/v1/genres/{id}` | Exclui um gênero | 204, 400, 404 |

### 👨‍💼 Autores (`/api/v1/authors`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/v1/authors` | Lista todos os autores com paginação | 200, 400 |
| `GET` | `/api/v1/authors/{id}` | Obtém um autor específico por ID | 200, 404 |
| `POST` | `/api/v1/authors` | Cria um novo autor | 201, 400 |
| `PUT` | `/api/v1/authors/{id}` | Atualiza um autor existente | 200, 400, 404 |
| `DELETE` | `/api/v1/authors/{id}` | Exclui um autor | 204, 400, 404 |

### 📘 Livros (`/api/v1/books`)

| Método | Endpoint | Descrição | Status Codes |
|--------|----------|-----------|--------------|
| `GET` | `/api/v1/books` | Lista todos os livros com paginação | 200, 400 |
| `GET` | `/api/v1/books/{id}` | Obtém um livro específico por ID | 200, 404 |
| `POST` | `/api/v1/books` | Cria um novo livro | 201, 400 |
| `PUT` | `/api/v1/books/{id}` | Atualiza um livro existente | 200, 400, 404 |
| `DELETE` | `/api/v1/books/{id}` | Exclui um livro | 204, 400, 404 |

### Paginação

Todos os endpoints GET de listagem suportam paginação através dos parâmetros:

- `page`: Número da página (padrão: 1)
- `pageSize`: Quantidade de itens por página (padrão: 20, máximo: 100)
- `paged`: Habilitar/desabilitar paginação (padrão: `true`). Quando `false`, retorna todos os registros ignorando `page` e `pageSize`

**Exemplos:**

Paginado (padrão):
```
GET /api/v1/books?page=1&pageSize=10
```

Todos os registros (sem paginação):
```
GET /api/v1/books?paged=false
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
git clone https://github.com/GabrielTancrede/ApiBook.git
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
POST /api/v1/genres
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
POST /api/v1/authors
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
GET /api/v1/books/1
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
GET /api/v1/books?page=1&pageSize=10
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

### Listar Todos os Livros (sem paginação)
```http
GET /api/v1/books?paged=false
```

**Resposta (200 OK):**
```json
{
  "data": [
    { "id": 1, "title": "Fundação", ... },
    { "id": 2, "title": "Eu, Robô", ... }
  ],
  "currentPage": 1,
  "totalPages": 1,
  "totalItens": 2,
  "totalItemsPage": 2,
  "maxItemsPerPage": 2,
  "previousPage": null,
  "nextPage": null,
  "isLast": true,
  "paged": false
}
```

### Atualizar um Gênero
```http
PUT /api/v1/genres/1
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
DELETE /api/v1/books/1
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
  "path": "/api/v1/genres/999"
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
┃ ┣ 📂 Entities
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
┣ 📂 Book.Tests
┃ ┣ 📂 Authors
┃ ┃ ┗ 📂 Handlers
┃ ┃   ┣ 📜 CreateAuthorHandlerTests.cs
┃ ┃   ┣ 📜 UpdateAuthorHandlerTests.cs
┃ ┃   ┣ 📜 DeleteAuthorHandlerTests.cs
┃ ┃   ┣ 📜 GetAuthorByIdHandlerTests.cs
┃ ┃   ┗ 📜 GetPagedAuthorsHandlerTests.cs
┃ ┣ 📂 Books
┃ ┃ ┗ 📂 Handlers
┃ ┃   ┣ 📜 CreateBookHandlerTests.cs
┃ ┃   ┣ 📜 UpdateBookHandlerTests.cs
┃ ┃   ┣ 📜 DeleteBookHandlerTests.cs
┃ ┃   ┣ 📜 GetBookByIdHandlerTests.cs
┃ ┃   ┗ 📜 GetPagedBooksHandlerTests.cs
┃ ┗ 📂 Genres
┃   ┗ 📂 Handlers
┃     ┣ 📜 CreateGenreHandlerTests.cs
┃     ┣ 📜 UpdateGenreHandlerTests.cs
┃     ┣ 📜 DeleteGenreHandlerTests.cs
┃     ┣ 📜 GetGenreByIdHandlerTests.cs
┃     ┗ 📜 GetPagedGenresHandlerTests.cs
┗ 📜 README.md
```

## 🧪 Testes

O projeto possui **50 testes unitários** cobrindo todos os handlers (Commands e Queries) das três entidades, utilizando **xUnit**, **Moq** e **FluentAssertions**.

### Tecnologias de Teste

| Pacote | Versão | Finalidade |
|--------|--------|------------|
| xUnit | 2.x | Framework de testes |
| Moq | 4.20.72 | Criação de mocks |
| FluentAssertions | 7.2.0 | Asserções legíveis e expressivas |

### Cobertura de Testes

| Feature | Create | Update | Delete | GetById | GetPaged | Duplicidade | Total |
|---------|:------:|:------:|:------:|:-------:|:--------:|:-----------:|:-----:|
| Gêneros | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | 18 |
| Autores | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | 16 |
| Livros  | ✓ | ✓ | ✓ | ✓ | ✓ | ✓ | 16 |
| **Total** | | | | | | | **50** |

### Cenários Testados

- ✅ Criação com dados válidos
- ✅ Atualização com dados válidos
- ✅ Exclusão de entidade existente
- ✅ Busca por ID existente e inexistente
- ✅ Listagem paginada
- ✅ Tentativa de exclusão com livros associados (gênero/autor)
- ✅ Tentativa de criação/atualização com nome duplicado (gênero/autor)
- ✅ Tentativa de criação/atualização com título duplicado para o mesmo autor (livro)
- ✅ Validação de entidade inexistente (update/delete)

### Executar os Testes

```bash
dotnet test
```

Ou com detalhes:

```bash
dotnet test --verbosity normal
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

**Desenvolvido com .NET 8 e Clean Architecture**
