using MusicStore.Entities;
using MusicStore.Entities.info;

namespace MusicStore.Repositories.interfaces
{
    public interface IConcertRepository : IRepositoryBase<Concert>
    {
        Task<ICollection<ConcertInfo>> GetAsync(string? title);
    }
}
