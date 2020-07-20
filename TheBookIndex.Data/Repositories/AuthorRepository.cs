using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBookIndex.Data.EFCore;
using TheBookIndex.Data.Models;

namespace TheBookIndex.Data.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _libraryContext;

        public AuthorRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _libraryContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthor(int id)
        {
            return await _libraryContext.Authors.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
