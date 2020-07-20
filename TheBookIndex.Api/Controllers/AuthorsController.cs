using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookIndex.Data.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheBookIndex.Api.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _authorRepository.GetAuthors());
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _authorRepository.GetAuthor(id));
        }

        // POST api/authors
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
