namespace Book.Application.DTOs.Genre
{
    public record CreateGenreDto(
        string Name,
        string? Description
    );
}
