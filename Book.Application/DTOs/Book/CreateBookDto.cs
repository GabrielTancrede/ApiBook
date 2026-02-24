namespace Book.Application.DTOs.Book
{
    public record CreateBookDto(
        string Title,
        string? Description,
        DateTime PublicationDate,
        string? ISBN,
        int AuthorId,
        int GenreId
    );
}
