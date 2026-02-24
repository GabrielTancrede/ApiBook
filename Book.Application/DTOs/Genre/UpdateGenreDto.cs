namespace Book.Application.DTOs.Genre
{
    public record UpdateGenreDto(
        int Id,
        string Name,
        string? Description
    );
}
