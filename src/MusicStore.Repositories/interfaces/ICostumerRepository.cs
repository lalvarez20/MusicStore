using MusicStore.Entities;


namespace MusicStore.Repositories.interfaces
{
    public interface ICostumerRepository : IRepositoryBase<Costumer>
    {
        Task<Costumer?> GetByEmailAsync(string email);
    }
}
