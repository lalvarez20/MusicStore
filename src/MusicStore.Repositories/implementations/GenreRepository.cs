
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.interfaces;

namespace MusicStore.Repositories.implementations;

public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext context) : base(context)
    {
    }
}
