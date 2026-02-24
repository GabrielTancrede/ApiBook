namespace Book.Application.ViewModels.Genre
{
    public class GenreWithBooksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<BookSimpleViewModel> Books { get; set; } = new();
    }

    public class BookSimpleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ISBN { get; set; }
    }
}
