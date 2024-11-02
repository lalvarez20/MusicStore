using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using System.Reflection;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertRepository repository;
        private readonly IGenreRepository genreRepository;
        private readonly ILogger<ConcertsController> logger;

        public ConcertsController(
            IConcertRepository repository,
            IGenreRepository genreRepository,
            ILogger<ConcertsController> logger)
        {
            this.repository = repository;
            this.genreRepository = genreRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var concertDB = await repository.GetAsync();

            var concerts = concertDB.Select(x => new ConcertResponseDto
            {
                Title = x.Title,
                Description = x.Description,
                Place = x.Place,
                UnitPrice = x.UnitPrice,
                GenreId = x.GenreId,
                DateEvent = x.DateEvent,
                ImageUrl = x.ImageUrl,
                TicketsQuantity = x.TicketsQuantity
            }).ToList();

            return Ok(concertDB);
        }

        [HttpGet("title")]
        public async Task<IActionResult> Get(string? title)
        {
            var response = new BaseResponseGeneric<ICollection<ConcertResponseDto>>();
            try
            {
                var info = await repository.GetAsync(title);
                return Ok(info);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al ontener la información";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConcertRequestDto requestDto)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                //validatin requestId
                var genreDb = await genreRepository.GetAsync(requestDto.GenreId);
                if (genreDb is null)
                {
                    response.ErrorMessage = $"El id del género {requestDto.GenreId} es incorrecto";
                    logger.LogWarning(response.ErrorMessage);
                    return BadRequest(response);
                }

                var concertDb = new Concert
                {
                    Title = requestDto.Title,
                    Description = requestDto.Description,
                    Place = requestDto.Place,
                    UnitPrice = requestDto.UnitPrice,
                    GenreId = requestDto.GenreId,
                    DateEvent = requestDto.DateEvent,
                    ImageUrl = requestDto.ImageUrl,
                    TicketsQuantity = requestDto.TicketsQuantity
                };

                response.Data = await repository.AddAsync(concertDb);
                response.Success = true;

                return Ok(response);

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al registrar la información";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }

        }
    }
}
