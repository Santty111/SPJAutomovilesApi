using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using SPJAutomovilesApi.Data;
using SPJ_ProyectoMVC.Models;
namespace SPJAutomovilesApi.Controllers;

public static class CatalogoEndpoints
{
    public static void MapCatalogoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Catalogo").WithTags(nameof(Catalogo));

        group.MapGet("/", async (SPJAutomovilesApiContext db) =>
        {
            return await db.Catalogo.ToListAsync();
        })
        .WithName("GetAllCatalogos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Catalogo>, NotFound>> (int catalogoid, SPJAutomovilesApiContext db) =>
        {
            return await db.Catalogo.AsNoTracking()
                .FirstOrDefaultAsync(model => model.CatalogoId == catalogoid)
                is Catalogo model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCatalogoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int catalogoid, Catalogo catalogo, SPJAutomovilesApiContext db) =>
        {
            var affected = await db.Catalogo
                .Where(model => model.CatalogoId == catalogoid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.CatalogoId, catalogo.CatalogoId)
                    .SetProperty(m => m.Marca, catalogo.Marca)
                    .SetProperty(m => m.Modelo, catalogo.Modelo)
                    .SetProperty(m => m.Usado, catalogo.Usado)
                    .SetProperty(m => m.Precio, catalogo.Precio)
                    .SetProperty(m => m.IVA, catalogo.IVA)
                    .SetProperty(m => m.ImagePath, catalogo.ImagePath)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCatalogo")
        .WithOpenApi();

        group.MapPost("/", async (Catalogo catalogo, SPJAutomovilesApiContext db) =>
        {
            db.Catalogo.Add(catalogo);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Catalogo/{catalogo.CatalogoId}",catalogo);
        })
        .WithName("CreateCatalogo")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int catalogoid, SPJAutomovilesApiContext db) =>
        {
            var affected = await db.Catalogo
                .Where(model => model.CatalogoId == catalogoid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCatalogo")
        .WithOpenApi();
    }
}
