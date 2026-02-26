using System.ComponentModel.DataAnnotations;

namespace Book.Application.DTOs.Book
{
    public class CreateBookDto
    {
        [Required(ErrorMessage = "O título do livro é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O título deve ter no máximo 300 caracteres.")]
        public string Title { get; set; }

        [MaxLength(2000, ErrorMessage = "A descrição deve ter no máximo 2000 caracteres.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "A data de publicação é obrigatória.")]
        public DateTime PublicationDate { get; set; }

        [RegularExpression(@"^(?:\d[-]?){9}[\dX]$|^(?:\d[-]?){12}\d$",
            ErrorMessage = "O ISBN deve estar no formato ISBN-10 (ex: 0-306-40615-2) ou ISBN-13 (ex: 978-65-5939-484-6).")]
        public string? ISBN { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O AuthorId deve ser um valor válido maior que zero.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O GenreId deve ser um valor válido maior que zero.")]
        public int GenreId { get; set; }
    }
}