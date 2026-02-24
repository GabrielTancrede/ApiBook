namespace Book.Application.DTOs.Author
{
    public record CreateAuthorDto(
        string Name,
        string? Biography,
        DateTime? BirthDate
    );
}
