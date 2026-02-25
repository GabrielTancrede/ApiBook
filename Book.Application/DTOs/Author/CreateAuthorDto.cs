namespace Book.Application.DTOs.Author
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }
        public string? Biography { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
