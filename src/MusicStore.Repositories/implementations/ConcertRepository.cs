using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.info;
using MusicStore.Persistence;
using MusicStore.Repositories.interfaces;

namespace MusicStore.Repositories.implementations
{
    public class ConcertRepository : RepositoryBase<Concert>, IConcertRepository
    {
        public ConcertRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<ICollection<Concert>> GetAsync()
        {
            //eager loading --> Consulta toda la infomacion relacionada siempre
            return await context.Set<Concert>()
                .Include(x => x.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<ConcertInfo>> GetAsync(string? title)
        {
            //eager loading optimized
            //return await context.Set<Concert>()
            //    .Include(x => x.Genre)
            //    .Where(x => x.Title.Contains(title ?? string.Empty))
            //    .AsNoTracking()
            //    .Select(x => new ConcertInfo
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Description = x.Description,
            //        Place = x.Place,
            //        UnitPrice = x.UnitPrice,
            //        GenreName = x.Genre.Name,
            //        GenreId = x.Genre.Id,
            //        DateEvent = x.DateEvent.ToShortDateString(),
            //        TimeEvent = x.DateEvent.ToShortTimeString(),
            //        ImageUrl = x.ImageUrl,
            //        TicketsQuantity = x.TicketsQuantity,
            //        Finalized = x.Finalized,
            //        Status = x.Status ? "Activo" : "Inactivo"
            //    })
            //    .ToListAsync();

            //lazzy loading --> NO se coloca el include. El propio framwork detecta las relaciones y trae la data
            return await context.Set<Concert>()
                //.Include(x => x.Genre)
                .Where(x => x.Title.Contains(title ?? string.Empty))
                .AsNoTracking()
                .Select(x => new ConcertInfo
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Place = x.Place,
                    UnitPrice = x.UnitPrice,
                    GenreName = x.Genre.Name,
                    GenreId = x.Genre.Id,
                    DateEvent = x.DateEvent.ToShortDateString(),
                    TimeEvent = x.DateEvent.ToShortTimeString(),
                    ImageUrl = x.ImageUrl,
                    TicketsQuantity = x.TicketsQuantity,
                    Finalized = x.Finalized,
                    Status = x.Status ? "Activo" : "Inactivo"
                })
                .ToListAsync();
        }
    }
}
