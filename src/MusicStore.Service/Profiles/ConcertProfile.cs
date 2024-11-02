using AutoMapper;
using MusicStore.Dto.Response;
using MusicStore.Entities.info;

namespace MusicStore.Service.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            CreateMap<ConcertInfo, ConcertResponseDto>(); //se indica la entidad orige > destino
        }
    }
}
