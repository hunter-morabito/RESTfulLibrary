using Library.API.Models;
using Library.API.Services;
using Library.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _libraryRepository.GetAuthors();

            // map the entities to the Dtos
            var authors = Mapper.Map<IEnumerable<AuthorsDto>>(authorsFromRepo);
            return Ok(authors);
        }

        // forward slash is added automatically
        [HttpGet("{id}")]
        public IActionResult GetAuthor(Guid id) // Parameter needs to be same name as given in routing
        {
            var authorFromRepo = _libraryRepository.GetAuthor(id);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var author = Mapper.Map<AuthorsDto>(authorFromRepo);
            return Ok(author);
        }

    }
}
