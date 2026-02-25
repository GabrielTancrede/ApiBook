using System.ComponentModel.DataAnnotations;

namespace Book.Application.DTOs.Genre
{
    public class CreateGenreDto
    {
        [Required(ErrorMessage = "O nome do gênero é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do gênero deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Description { get; set; }
    }
}
