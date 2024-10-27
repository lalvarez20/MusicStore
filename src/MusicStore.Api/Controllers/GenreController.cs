using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository repository;

        public GenreController(IGenreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await repository.GetAsync();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await repository.GetAsync(id);
            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Genre genre)
        {
            await repository.AddAsync(genre);
            return Ok(genre);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Genre genre)
        {
            await repository.UpdateAsync(id, genre);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsyn(id);
            return Ok();
        }
    }
}
