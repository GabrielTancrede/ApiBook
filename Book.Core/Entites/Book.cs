using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Core.Entites
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ISBN { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int AuthorId { get; set; }
        public int GenreId { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
