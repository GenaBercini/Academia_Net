using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class SubjectEndpoints
    {
        public static void MapSubjectEndpoints(this WebApplication app)
        {
            app.MapGet("/subjects/{id}", async (int id, SubjectService subjectService) =>
            {
                //var service = new SubjectService();

                SubjectDTO? dto = await subjectService.GetAsync(id);

                if (dto == null)
                    return Results.NotFound();

                return Results.Ok(dto);
            })
            .WithName("GetSubject")
            .Produces<SubjectDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/subjects", async (SubjectService subjectService) =>
            {
                //var service = new SubjectService();
                var dtos = await subjectService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllSubjects")
            .Produces<List<SubjectDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/subjects", async (SubjectDTO dto, SubjectService subjectService) =>
            {
                try
                {
                    //var service = new SubjectService();
                    SubjectDTO created = await subjectService.AddAsync(dto);

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

            app.MapPut("/subjects", async (SubjectDTO dto, SubjectService subjectService) =>
            {
                try
                {
                    //var service = new SubjectService();
                    var updated = await subjectService.UpdateAsync(dto);

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

            app.MapDelete("/subjects/{id}", async (int id, SubjectService subjectService) =>
            {
                //var service = new SubjectService();
                var deleted = await subjectService.DeleteAsync(id);

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
