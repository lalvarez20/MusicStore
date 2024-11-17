
using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using MusicStore.Service.interfaces;

namespace MusicStore.Service.implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenreService> logger;
        private readonly IMapper mapper;

        public GenreService(
            IGenreRepository repository,
            ILogger<GenreService> logger,
            IMapper mapper
            )
        {
            this.repository = repository;
            this.logger = logger;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();
            try
            {
                var data = await repository.GetAsync();
                response.Data = mapper.Map<ICollection<GenreResponseDto>>(data);
                response.Success = true;

                logger.LogInformation($"Obteniendo los datos del generos");
            }
            catch(Exception ex) {
                response.ErrorMessage = "Ocurrió un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data = mapper.Map<GenreResponseDto>(data);
                response.Success = data is not null;

                logger.LogInformation($"Obteniendo los datos del genero por id {id}");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"Ocurrió un error al obtener los datos por id {id}";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                response.Data = await repository.AddAsync(mapper.Map<Genre>(request));
                response.Success = true;

                logger.LogInformation($"Registro de genero exitoso");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al registar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null) {
                    response.ErrorMessage = $"No se encontró el genero con id {id}";
                    logger.LogInformation(response.ErrorMessage);
                    return response;
                }

                mapper.Map(request, data);
                await repository.UpdateAsync();
                response.Success = true;

                logger.LogInformation("Genero actualizado correctamente");

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = $"No se encontró el genero con id {id}";
                    logger.LogInformation(response.ErrorMessage);
                    return response;
                }

                await repository.DeleteAsync(id);
                response.Success = true;

                logger.LogInformation("Genero eliminado correctamente");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al eliminar los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
