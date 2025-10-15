using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class SubjectEndpoints
    {
        public static void MapSubjectEndpoints(this WebApplication app)
        {
            app.MapGet("/subjects/{id}", (int id) =>
            {
                var service = new SubjectService();

                SubjectDTO? dto = service.Get(id);

                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetSubject")
            .Produces<SubjectDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/subjects", () =>
            {
                var service = new SubjectService();
                var dtos = service.GetAll();

                return Results.Ok(dtos);
            })
            .WithName("GetAllSubjects")
            .Produces<List<SubjectDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/subjects", (SubjectDTO dto) =>
            {
                try
                {
                    var service = new SubjectService();
                    SubjectDTO created = service.Add(dto);

                    return Results.Created($"/subjects/{created.Id}", created);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddSubject")
            .Produces<SubjectDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/subjects", (SubjectDTO dto) =>
            {
                try
                {
                    var service = new SubjectService();
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
            .WithName("UpdateSubject")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/subjects/{id}", (int id) =>
            {
                var service = new SubjectService();
                var deleted = service.Delete(id);

                if (!deleted)
                    return Results.NotFound();

                return Results.NoContent();
            })
            .WithName("DeleteSubject")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
