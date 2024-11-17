﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using MusicStore.Service.interfaces;
using System.Runtime.InteropServices;

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

        public async Task<BaseResponseGeneric<ConcertResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<ConcertResponseDto>();
            try
            {
                var data = await repository.GetAsync(id);
                response.Data =mapper.Map<ConcertResponseDto>(data);
                response.Success = true;

                logger.LogInformation($"Obteniendo los datos por id: {id}");
            }
            catch (Exception ex) {
                response.ErrorMessage = "Ocurrió un error al obtener los datos";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(ConcertRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();
            try { 
                response.Data = await repository.AddAsync(mapper.Map<Concert>(request));
                response.Success = true;

                logger.LogInformation($"Se guardó la información del concierto");
            }
            catch(Exception ex) {
                response.ErrorMessage = "Ocurrió un error registrar el concierto";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;

        }

        public async Task<BaseResponse> UpdateAsync(int id, ConcertRequestDto request)
        {
            var response = new BaseResponse();
            try { 
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = "El registro no fue encontrado";
                    return response;
                }

                mapper.Map(response, data);
                await repository.UpdateAsync();
                response.Success = true;

                logger.LogInformation($"Se actualizó la información del concierto");
            }
            catch(Exception ex) {
                response.ErrorMessage = "Ocurrió un error actualizar el concierto";
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
                    response.ErrorMessage = "El registro no fue encontrado";
                    return response;
                }

                await repository.DeleteAsync(id);
                response.Success = true;

                logger.LogInformation($"Se eliminó la información del concierto con id {id}");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al eliminar el concierto";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> FinalizeAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var data = await repository.GetAsync(id);
                if (data is null)
                {
                    response.ErrorMessage = "El registro no fue encontrado";
                    return response;
                }

                await repository.FinalizeAsync(id);
                response.Success = true;

                logger.LogInformation($"Se finalizó del concierto con id {id}");
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al finalizar el concierto";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

    }
}
