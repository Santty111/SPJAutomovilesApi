using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPJAutomovilesApi.Data;
using SPJAutomovilesApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<SPJAutomovilesApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SPJAutomovilesApiContext") ?? throw new InvalidOperationException("Connection string 'SPJAutomovilesApiContext' not found.")));

// Añadir servicios al contenedor
builder.Services.AddControllers();

// Configuración Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()   // Permitir cualquier origen
               .AllowAnyMethod()   // Permitir cualquier método (GET, POST, etc.)
               .AllowAnyHeader();  // Permitir cualquier cabecera
    });
});

var app = builder.Build();

// Configurar la tubería de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar CORS (esto debe estar antes de app.UseAuthorization())
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Mapear tus endpoints de catálogo (si es necesario)
app.MapCatalogoEndpoints();

app.Run();
