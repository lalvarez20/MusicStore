using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using System.Net;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenreController> logger;

        public GenreController(IGenreRepository repository, ILogger<GenreController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();
            try
            {
                //mapping db to Dto
                var genreDb = await repository.GetAsync();

                var genres = genreDb.Select(x => new GenreResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).ToList();

                response.Data = genres;
                response.Success = true;
                logger.LogInformation($"Obteniendo todos lod géneros musicales.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al consultar la infomación.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDto>();
            try
            {
                //mapping db to Dto
                var genreDb = await repository.GetAsync(id);
                if (genreDb is null)
                {
                    logger.LogWarning($"Género musical con id: {id} no existe");
                    response.ErrorMessage = $"No se encontró el registro";
                    return NotFound(response);
                }

                var genre = new GenreResponseDto
                {
                    Id = genreDb.Id,
                    Name = genreDb.Name,
                    Status = genreDb.Status
                };

                response.Data = genre;
                response.Success = true;
                logger.LogInformation($"Obteniendo género musical con id: {id}");
                return response.Data is not null ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al consultar la infomación.";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreRequestDto genre)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                //mapping Dto to DB
                var genreId = await repository.AddAsync(new Genre
                {
                    Name = genre.Name,
                    Status = genre.Status,
                });

                response.Data = genreId;
                response.Success = true;
                logger.LogInformation($"Género musical creado con id: {genreId}");
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al crear";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, GenreRequestDto genre)
        {
            var response = new BaseResponse();
            try
            {

                var genreBd = await repository.GetAsync(id);

                if (genreBd is null) {
                    logger.LogWarning($"Género musical con id: {id} no existe");
                    response.ErrorMessage = $"No se encontró el registro";
                    return NotFound(response);
                }

                genreBd.Name = genre.Name;
                genreBd.Status = genre.Status;

                await repository.UpdateAsync();
                response.Success = true;
                logger.LogInformation($"Género musical con id: {id} actualizado");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();
            try
            {
                var genreBd = await repository.GetAsync(id);

                if (genreBd is null)
                {
                    logger.LogWarning($"Género musical con id: {id} no existe");
                    response.ErrorMessage = $"No se encontró el registro";
                    return NotFound(response);
                }

                await repository.DeleteAsync(id);
                response.Success = true;
                logger.LogInformation($"Género musical con id: {id} eliminado");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al elimnar";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
    }
}
