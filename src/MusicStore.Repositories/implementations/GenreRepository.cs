using Microsoft.EntityFrameworkCore;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
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
    public async Task<List<GenreResponseDto>> GetAsync()
    {
        var items = await context.Genres.ToListAsync();

        //mapping Entity to Dto
        var response = items.Select(x => new GenreResponseDto
        {
            Id = x.Id,
            Name = x.Name,
            Status = x.Status
        }).ToList();

        return response;
    }

    public async Task<GenreResponseDto?> GetAsync(int id) //el signo ? en el nombre del método indica que puede devolver un valor Nulo
    {
        var item = await context.Genres.FirstOrDefaultAsync(x => x.Id == id); // FisrtOrDefault --> en caso no encuentre el Id enviado devuelve Nulo

        if (item is not null)
            //mapping
            return new GenreResponseDto
            {
                Id= item.Id,
                Name = item.Name,
                Status = item.Status
            };
        else
            throw new InvalidOperationException($"No se encontro el registro con id: {id}");
    }

    public async Task<int> AddAsync(GenreRequestDto genreDto)
    {
        //mapping
        var item = new Genre
        {
            Name = genreDto.Name,
            Status = genreDto.Status,
        };
        context.Genres.Add(item);
        await context.SaveChangesAsync();
        return item.Id;
    }

    public async Task UpdateAsync(int id, GenreRequestDto genreDto)
    {
        var item = await context.Genres.FirstOrDefaultAsync(x=> x.Id == id);
        if (item is not null)  // Se prefiere el uso de is not null en lugar de !=
        {
            item.Name = genreDto.Name;
            item.Status = genreDto.Status;

            context.Genres.Update(item);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"No se encontro el registro con id: {id}");
        }
    }

    public async Task DeleteAsyn(int id)
    {
        var item = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (item is not null)
        {
            context.Genres.Remove(item);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"No se encontro el registro con id: {id}");
        }
    }
}
