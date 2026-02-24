using Book.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace Book.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Core.Entites.Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Genre Configuration
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
                
                entity.HasMany(e => e.Books)
                    .WithOne(b => b.Genre)
                    .HasForeignKey(b => b.GenreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Author Configuration
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Biography).HasMaxLength(2000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
                
                entity.HasMany(e => e.Books)
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Book Configuration
            modelBuilder.Entity<Core.Entites.Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.ISBN).HasMaxLength(20);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
                
                entity.HasOne(e => e.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(e => e.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.Genre)
                    .WithMany(g => g.Books)
                    .HasForeignKey(e => e.GenreId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
