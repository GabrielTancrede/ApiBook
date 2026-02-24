namespace Book.Application.DTOs.Author
{
    public record UpdateAuthorDto(
        int Id,
        string Name,
        string? Biography,
        DateTime? BirthDate
    );
}
