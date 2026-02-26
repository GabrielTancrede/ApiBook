using System.ComponentModel.DataAnnotations;

namespace Book.Application.ModelInputs
{
    public class PaginationRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "A página deve ser maior ou igual a 1.")]
        public int Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "O tamanho da página deve ser entre 1 e 100.")]
        public int PageSize { get; set; } = 20;

        public bool Paged { get; set; } = true;
    }
}
