using Microsoft.EntityFrameworkCore;
using MusicStore.Persistence;
using MusicStore.Repositories.implementations;
using MusicStore.Repositories.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//Region permite agrupar bloques de codigo
#region Registering Services
builder.Services.AddScoped<IGenreRepository, GenreRepository>(); // Se modifica la inyeci�n de dependencia cambiando a Scope y de agrega tanto la interface como la clase
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();