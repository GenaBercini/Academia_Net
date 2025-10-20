using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class SpecialtyEndpoints
    {
        public static void MapSpecialtyEndpoints(this WebApplication app)
        {
            app.MapGet("/specialties/{id}", (int id) =>
            {
                var service = new SpecialtyService();

                SpecialtyDTO? dto = service.Get(id);

                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetSpecialty")
            .Produces<SpecialtyDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/specialties", () =>
            {
                var service = new SpecialtyService();
                var dtos = service.GetAll();

                return Results.Ok(dtos);
            })
            .WithName("GetAllSpecialties")
            .Produces<List<SpecialtyDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/specialties", (SpecialtyDTO dto) =>
            {
                try
                {
                    var service = new SpecialtyService();
                    SpecialtyDTO created = service.Add(dto);

                    return Results.Created($"/specialties/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddSpecialty")
            .Produces<SpecialtyDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/specialties", (SpecialtyDTO dto) =>
            {
                try
                {
                    var service = new SpecialtyService();
                    var updated = service.Update(dto);

                    if (!updated)
                        return Results.NotFound();

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateSpecialty")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/specialties/{id}", (int id) =>
            {
                var service = new SpecialtyService();
                var deleted = service.Delete(id);

                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteSpecialty")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
