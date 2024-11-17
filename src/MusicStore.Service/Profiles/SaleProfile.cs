using AutoMapper;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using System.Globalization;

namespace MusicStore.Service.Profiles
{
    public class SaleProfile : Profile
    {
        private static readonly CultureInfo cultureInfo = new CultureInfo("en-PE"); //ddmmaaaa personalizacion de fecha
        public SaleProfile()
        {
            CreateMap<SaleRequestDto, Sale>()
                    .ForMember(d => d.Quantity, o => o.MapFrom(x => x.TicketsQuantity));

            CreateMap<Sale, SaleResponseDto>()
                .ForMember(d => d.SaleId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.Concert.DateEvent.ToString("D", cultureInfo)))
                .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.Concert.DateEvent.ToString("T", cultureInfo)))
                .ForMember(d => d.GenreName, o => o.MapFrom(x => x.Concert.Genre.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(x => x.Concert.ImageUrl))
                .ForMember(d => d.Title, o => o.MapFrom(x => x.Concert.Title))
                .ForMember(d => d.FullName, o => o.MapFrom(x => x.Costumer.FullName))
                .ForMember(d => d.SaleDate, o => o.MapFrom(x => x.SaleDate.ToString("dd/mm/yyyy HH:mm", cultureInfo)));
        }

    }
}
