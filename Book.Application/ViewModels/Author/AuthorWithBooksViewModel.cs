namespace Book.Application.ViewModels.Author
{
    public class AuthorWithBooksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public DateTime? BirthDate { get; set; }
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
