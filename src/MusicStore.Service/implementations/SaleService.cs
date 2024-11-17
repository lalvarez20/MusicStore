using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using MusicStore.Service.interfaces;

namespace MusicStore.Service.implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository repository;
        private readonly ILogger<SaleService> logger;
        private readonly IMapper mapper;
        private readonly IConcertRepository concertRepository;
        private readonly ICostumerRepository costumerRepository;

        public SaleService(
            ISaleRepository repository,
            ILogger<SaleService> logger,
            IMapper mapper,
            IConcertRepository concertRepository,
            ICostumerRepository costumerRepository
            )
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.concertRepository = concertRepository;
            this.costumerRepository = costumerRepository;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                await repository.CreateTransactionAsync();
                var entity = mapper.Map<Sale>(request);

                var costumer = await costumerRepository.GetByEmailAsync(email);
                if (costumer is null)
                {
                    costumer = new Costumer()
                    {
                        Email = email,
                        FullName = request.FullName
                    };
                    costumer.Id = await costumerRepository.AddAsync(costumer);
                }

                entity.CostumerId = costumer.Id;

                var concert = await concertRepository.GetAsync(request.ConcertId);
                if (concert is null)
                    throw new Exception($"El concierto con id {request.ConcertId} no existe");

                if (DateTime.Today > concert.DateEvent)
                    throw new InvalidOperationException($"No se puede comprar tickets pata el concierto {concert.Title} porque ya pasó");

                if (concert.Finalized)
                    throw new Exception($"El concierto con id {request.ConcertId} ya finalizó");

                entity.Total = entity.Quantity * (decimal)concert.UnitPrice;

                await repository.AddAsync(entity);
                await repository.UpdateAsync();

                response.Success = true;
                response.Data = entity.Id;

                logger.LogInformation("Se creó correctamente la venta para {email}", email);
            }
            catch (InvalidOperationException ex)
            {
                await repository.RollBackAsync();
                response.ErrorMessage = ex.Message;

                logger.LogWarning(ex, "{ErrorMessage}", response.ErrorMessage);
            }
            catch (Exception ex)
            {
                await repository.RollBackAsync();
                response.ErrorMessage = "Error al crear la venta";

                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<SaleResponseDto>();

            try
            {
                var sale = await repository.GetAsync(id);
                response.Data = mapper.Map<SaleResponseDto>(sale);
                response.Success = response.Data is not null;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al seleccionar la venta";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);

            }

            return response;
        }
    }
}
