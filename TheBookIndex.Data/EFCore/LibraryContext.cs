using Microsoft.EntityFrameworkCore;
using TheBookIndex.Data.BaseTypes;
using TheBookIndex.Data.Models;

namespace TheBookIndex.Data.EFCore
{
    public class LibraryContext : DbContext
    {
        private readonly ConnectionString _connectionString;

        public LibraryContext(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString.Db);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LastName).IsRequired();
            });
        }
    }
}
