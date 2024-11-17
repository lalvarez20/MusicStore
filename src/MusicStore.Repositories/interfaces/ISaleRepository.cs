
using MusicStore.Entities;

namespace MusicStore.Repositories.interfaces
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransactionAsync();
        Task RollBackAsync();
    }
}
