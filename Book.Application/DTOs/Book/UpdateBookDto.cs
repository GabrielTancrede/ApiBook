namespace Book.Application.DTOs.Book
{
    public record UpdateBookDto(
        int Id,
        string Title,
        string? Description,
        DateTime PublicationDate,
        string? ISBN,
        int AuthorId,
        int GenreId
    );
}
