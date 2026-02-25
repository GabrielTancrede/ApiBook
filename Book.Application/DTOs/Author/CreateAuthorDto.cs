using System.ComponentModel.DataAnnotations;

namespace Book.Application.DTOs.Author
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "O nome do autor é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome do autor deve ter no máximo 200 caracteres.")]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = "A biografia deve ter no máximo 2000 caracteres.")]
        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
