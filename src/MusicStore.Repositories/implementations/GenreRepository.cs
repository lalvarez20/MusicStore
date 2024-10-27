using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.interfaces;

namespace MusicStore.Repositories.implementations;

public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext context;

    public GenreRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<List<Genre>> GetAsync()
    {
        return await context.Genres.ToListAsync();
    }

    public async Task<Genre?> GetAsync(int id) //el signo ? en el nombre del método indica que puede devolver un valor Nulo
    {
        return await context.Genres.FirstOrDefaultAsync(x => x.Id == id); // FisrtOrDefault --> en caso no encuentre el Id enviado devuelve Nulo
    }

    public async Task<int> AddAsync(Genre genre)
    {
        context.Genres.Add(genre);
        await context.SaveChangesAsync();
        return genre.Id;
    }

    public async Task UpdateAsync(int id, Genre genre)
    {
        var item = await GetAsync(id);
        if (item is not null)  // Se prefiere el uso de is not null en lugar de !=
        {
            item.Name = genre.Name;
            item.Status = genre.Status;

            context.Genres.Update(item);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsyn(int id)
    {
        var item = await GetAsync(id);
        if (item is not null)
        {
            context.Genres.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
