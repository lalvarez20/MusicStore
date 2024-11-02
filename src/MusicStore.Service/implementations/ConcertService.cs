using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Repositories.interfaces;
using MusicStore.Service.interfaces;

namespace MusicStore.Service.implementations
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository repository;
        private readonly ILogger<ConcertService> logger;
        private readonly IMapper mapper;

        public ConcertService(
            IConcertRepository repository,
            ILogger<ConcertService> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<BaseResponseGeneric<ICollection<ConcertResponseDto>>> GetAsync(string? title)
        {
            var response = new BaseResponseGeneric<ICollection<ConcertResponseDto>>();
            try
            {
                var data = await repository.GetAsync(title);
                response.Data = mapper.Map<ICollection<ConcertResponseDto>>(data); // mapping
                response.Success = true;

                logger.LogInformation($"Obteniendo los datos por titulo: {title ?? string.Empty}");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public Task<BaseResponseGeneric<ConcertResponseDto>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<BaseResponseGeneric<int>> AddAsync(ConcertRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateAsync(int id, ConcertRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> FinalizeAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
