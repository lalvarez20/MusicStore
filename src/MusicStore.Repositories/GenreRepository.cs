using MusicStore.Entities;

namespace MusicStore.Repositories;

public class GenreRepository
{
    private readonly List<Genre> _genreList;

    public GenreRepository()
    {
        _genreList = new(); //se puede utiizar la instancia del objeto sin indicar el tipo, siempre que se haya declarado la variable anteriormente
        _genreList.Add(new Genre() { Id = 1, Name = "Salsa" });
        _genreList.Add(new Genre() { Id = 2, Name = "Pop" });
        _genreList.Add(new Genre() { Id = 5, Name = "Cumbia" });
    }
    public List<Genre> Get()
    {
        return _genreList;
    }

    public Genre? Get(int id) //el signo ? en el nombre del método indica que puede devolver un valor Nulo
    {
        return _genreList.FirstOrDefault(x => x.Id == id); // FisrtOrDefault --> en caso no encuentre el Id enviado devuelve Nulo
    }

    public void Add(Genre genre)
    {
        var lastIterm = _genreList.MaxBy(x => x.Id); // Obtiene el elmento con el mayor Id
        genre.Id = lastIterm is null ? 1 : lastIterm.Id + 1;
        _genreList.Add(genre);
    }

    public void Update(int id, Genre genre)
    {
        var item = Get(id);
        if (item is not null)  // Se prefiere el uso de is not null en lugar de !=
        {
            item.Name = genre.Name;
            item.Status = genre.Status;
        }
    }

    public void Delete(int id)
    {
        var item = Get(id);
        if (item is not null)
        {
            _genreList.Remove(item);
        }
    }
}
