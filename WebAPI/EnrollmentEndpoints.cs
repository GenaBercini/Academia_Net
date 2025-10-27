using Application.Services;
using DTOs;
using Microsoft.AspNetCore.Mvc; 

namespace WebAPI
{
    public static class EnrollmentEndpoints
    {
        public static void MapEnrollmentEndpoints(this WebApplication app)
        {
            app.MapGet("/userCourseSubjects", async (int userId, int courseId, [FromServices] EnrollmentService service) =>
            {
                try
                {
                    var dtos = await service.GetEnrollmentsByUserAndCourseAsync(userId, courseId);

                    if (dtos == null || !dtos.Any())
                        return Results.NotFound(new { mensaje = $"No se encontraron inscripciones para el usuario {userId} en el curso {courseId}." });

                    return Results.Ok(dtos);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al obtener inscripciones del usuario {userId} en curso {courseId}. Detalle: {ex.Message}");
                }
            })
            .WithName("GetUserCourseSubjects")
            .Produces<IEnumerable<UserCourseSubjectDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            app.MapPost("/userCourseSubjects", async (UserCourseSubjectDTO dto, [FromServices] EnrollmentService service) =>
            {
                try
                {
                    await service.AddEnrollmentAsync(dto);
                    return Results.Created($"/userCourseSubjects?userId={dto.UserId}&courseId={dto.CourseId}", dto);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al crear la inscripción: {ex.Message}");
                }
            })
            .WithName("AddUserCourseSubject")
            .Produces<UserCourseSubjectDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            app.MapDelete("/userCourseSubjects", async (int userId, int courseId, int subjectId, [FromServices] EnrollmentService service) =>
            {
                try
                {
                    await service.DeleteEnrollmentAsync(userId, courseId, subjectId);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { mensaje = "La inscripción especificada no existe." });
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Error al eliminar la inscripción: {ex.Message}");
                }
            })
            .WithName("DeleteUserCourseSubject")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
        }
    }
}
