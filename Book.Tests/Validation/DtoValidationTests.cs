using System.ComponentModel.DataAnnotations;
using Book.Application.DTOs.Author;
using Book.Application.DTOs.Book;
using Book.Application.DTOs.Genre;
using Book.Application.ModelInputs;
using FluentAssertions;

namespace Book.Tests.Validation
{
    public class DtoValidationTests
    {
        private static IList<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        #region Genre DTOs

        [Fact]
        public void CreateGenreDto_SemNome_DeveRetornarErro()
        {
            var dto = new CreateGenreDto { Name = null! };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("nome do gênero é obrigatório"));
        }

        [Fact]
        public void CreateGenreDto_NomeExcedeTamanho_DeveRetornarErro()
        {
            var dto = new CreateGenreDto { Name = new string('A', 101) };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("máximo 100 caracteres"));
        }

        [Fact]
        public void CreateGenreDto_DescricaoExcedeTamanho_DeveRetornarErro()
        {
            var dto = new CreateGenreDto { Name = "Válido", Description = new string('A', 501) };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("máximo 500 caracteres"));
        }

        [Fact]
        public void CreateGenreDto_DadosValidos_DevePassar()
        {
            var dto = new CreateGenreDto { Name = "Ficção", Description = "Descrição" };
            var results = ValidateModel(dto);
            results.Should().BeEmpty();
        }

        [Fact]
        public void UpdateGenreDto_SemNome_DeveRetornarErro()
        {
            var dto = new UpdateGenreDto { Name = null! };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("nome do gênero é obrigatório"));
        }

        #endregion

        #region Author DTOs

        [Fact]
        public void CreateAuthorDto_SemNome_DeveRetornarErro()
        {
            var dto = new CreateAuthorDto { Name = null! };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("nome do autor é obrigatório"));
        }

        [Fact]
        public void CreateAuthorDto_NomeExcedeTamanho_DeveRetornarErro()
        {
            var dto = new CreateAuthorDto { Name = new string('A', 201) };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("máximo 200 caracteres"));
        }

        [Fact]
        public void CreateAuthorDto_BiografiaExcedeTamanho_DeveRetornarErro()
        {
            var dto = new CreateAuthorDto { Name = "Válido", Biography = new string('A', 2001) };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("máximo 2000 caracteres"));
        }

        [Fact]
        public void CreateAuthorDto_DadosValidos_DevePassar()
        {
            var dto = new CreateAuthorDto { Name = "Machado de Assis", Biography = "Escritor brasileiro" };
            var results = ValidateModel(dto);
            results.Should().BeEmpty();
        }

        [Fact]
        public void UpdateAuthorDto_SemNome_DeveRetornarErro()
        {
            var dto = new UpdateAuthorDto { Name = null! };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("nome do autor é obrigatório"));
        }

        #endregion

        #region Book DTOs

        [Fact]
        public void CreateBookDto_SemTitulo_DeveRetornarErro()
        {
            var dto = new CreateBookDto
            {
                Title = null!,
                PublicationDate = DateTime.Now,
                AuthorId = 1,
                GenreId = 1
            };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("título do livro é obrigatório"));
        }

        [Fact]
        public void CreateBookDto_TituloExcedeTamanho_DeveRetornarErro()
        {
            var dto = new CreateBookDto
            {
                Title = new string('A', 301),
                PublicationDate = DateTime.Now,
                AuthorId = 1,
                GenreId = 1
            };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("máximo 300 caracteres"));
        }

        [Fact]
        public void CreateBookDto_AuthorIdZero_DeveRetornarErro()
        {
            var dto = new CreateBookDto
            {
                Title = "Livro Teste",
                PublicationDate = DateTime.Now,
                AuthorId = 0,
                GenreId = 1
            };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("AuthorId"));
        }

        [Fact]
        public void CreateBookDto_GenreIdZero_DeveRetornarErro()
        {
            var dto = new CreateBookDto
            {
                Title = "Livro Teste",
                PublicationDate = DateTime.Now,
                AuthorId = 1,
                GenreId = 0
            };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("GenreId"));
        }

        [Fact]
        public void CreateBookDto_ISBNExcedeTamanho_DeveRetornarErro()
        {
            var dto = new CreateBookDto
            {
                Title = "Livro Teste",
                PublicationDate = DateTime.Now,
                AuthorId = 1,
                GenreId = 1,
                ISBN = new string('1', 21)
            };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("máximo 20 caracteres"));
        }

        [Fact]
        public void CreateBookDto_DadosValidos_DevePassar()
        {
            var dto = new CreateBookDto
            {
                Title = "Dom Casmurro",
                PublicationDate = new DateTime(1899, 1, 1),
                AuthorId = 1,
                GenreId = 1,
                ISBN = "978-3-16-148410-0"
            };
            var results = ValidateModel(dto);
            results.Should().BeEmpty();
        }

        [Fact]
        public void UpdateBookDto_SemTitulo_DeveRetornarErro()
        {
            var dto = new UpdateBookDto
            {
                Title = null!,
                PublicationDate = DateTime.Now,
                AuthorId = 1,
                GenreId = 1
            };
            var results = ValidateModel(dto);
            results.Should().Contain(r => r.ErrorMessage!.Contains("título do livro é obrigatório"));
        }

        #endregion

        #region PaginationRequest

        [Fact]
        public void PaginationRequest_PaginaZero_DeveRetornarErro()
        {
            var request = new PaginationRequest { Page = 0, PageSize = 20 };
            var results = ValidateModel(request);
            results.Should().Contain(r => r.ErrorMessage!.Contains("página deve ser maior ou igual a 1"));
        }

        [Fact]
        public void PaginationRequest_PageSizeExcedeLimite_DeveRetornarErro()
        {
            var request = new PaginationRequest { Page = 1, PageSize = 101 };
            var results = ValidateModel(request);
            results.Should().Contain(r => r.ErrorMessage!.Contains("tamanho da página deve ser entre 1 e 100"));
        }

        [Fact]
        public void PaginationRequest_PageSizeZero_DeveRetornarErro()
        {
            var request = new PaginationRequest { Page = 1, PageSize = 0 };
            var results = ValidateModel(request);
            results.Should().Contain(r => r.ErrorMessage!.Contains("tamanho da página deve ser entre 1 e 100"));
        }

        [Fact]
        public void PaginationRequest_ValoresPadrao_DevePassar()
        {
            var request = new PaginationRequest();
            var results = ValidateModel(request);
            results.Should().BeEmpty();
        }

        #endregion
    }
}
