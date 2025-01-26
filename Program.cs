using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SPJAutomovilesApi.Data;
using SPJAutomovilesApi.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SPJAutomovilesApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SPJAutomovilesApiContext") ?? throw new InvalidOperationException("Connection string 'SPJAutomovilesApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapCatalogoEndpoints();

//app.MapCatalogoEndpoints();

app.Run();
