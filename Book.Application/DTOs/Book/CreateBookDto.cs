namespace Book.Application.DTOs.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ISBN { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
