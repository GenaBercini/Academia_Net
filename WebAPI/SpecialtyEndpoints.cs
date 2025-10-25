using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class SpecialtyEndpoints
    {
        public static void MapSpecialtyEndpoints(this WebApplication app)
        {
            app.MapGet("/specialties/{id}", async (int id, SpecialtyService specialtyService) =>
            {
                //var service = new SpecialtyService();

                SpecialtyDTO? dto =await specialtyService.GetAsync(id);

                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetSpecialty")
            .Produces<SpecialtyDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/specialties",async (SpecialtyService specialtyService) =>
            {
                //var service = new SpecialtyService();
                var dtos = await specialtyService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllSpecialties")
            .Produces<List<SpecialtyDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/specialties", async (SpecialtyDTO dto, SpecialtyService specialtyService) =>
            {
                try
                {
                    //var service = new SpecialtyService();
                    SpecialtyDTO created =await specialtyService.AddAsync(dto);

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

            app.MapPut("/specialties",async (SpecialtyDTO dto, SpecialtyService specialtyService) =>
            {
                try
                {
                    //var service = new SpecialtyService();
                    var updated = await specialtyService.UpdateAsync(dto);

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

            app.MapDelete("/specialties/{id}", async (int id, SpecialtyService specialtyService) =>
            {
                //var service = new SpecialtyService();
                var deleted = await specialtyService.DeleteAsync(id);

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
