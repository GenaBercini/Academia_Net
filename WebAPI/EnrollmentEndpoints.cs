using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class EnrollmentEndpoints
    {
        public static void MapEnrollmentEndpoints(this WebApplication app)
        {
            app.MapGet("/userCourseSubjects/{userId:int}/{courseId:int}/{subjectId:int}", async (
                int userId,
                int courseId,
                int subjectId,
                EnrollmentService service) =>
            {
                var enrollment = await service.GetEnrollmentAsync(userId, courseId, subjectId);
                if (enrollment == null)
                    return Results.NotFound();
                return Results.Ok(enrollment);
            })
            .WithName("GetUserCourseSubject")
            .Produces<UserCourseSubjectDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/userCourseSubjects", async (
                int? userId,
                int? courseId,
                EnrollmentService service) =>
            {
                if (userId.HasValue && courseId.HasValue)
                {
                    var dtos = await service.GetEnrollmentsByUserAndCourseAsync(userId.Value, courseId.Value);
                    return Results.Ok(dtos);
                }
                return Results.BadRequest(new { error = "Debe especificar userId y courseId para filtrar inscripciones." });
            })
            .WithName("GetUserCourseSubjects")
            .Produces<List<UserCourseSubjectDTO>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPost("/userCourseSubjects", async (UserCourseSubjectDTO dto, EnrollmentService service) =>
            {
                try
                {
                    await service.AddEnrollmentAsync(dto);
                    return Results.Created($"/userCourseSubjects/{dto.UserId}/{dto.CourseId}/{dto.SubjectId}", dto);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddUserCourseSubject")
            .Produces<UserCourseSubjectDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/userCourseSubjects", async (UserCourseSubjectDTO dto, EnrollmentService service) =>
            {
                try
                {
                    var updated = await service.UpdateAsync(dto);
                    if (!updated)
                        return Results.NotFound();
                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateUserCourseSubject")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/userCourseSubjects/{userId:int}/{courseId:int}/{subjectId:int}", async (
                        int userId,
                        int courseId,
                        int subjectId,
            EnrollmentService service) =>
            {
                try
                {
                    await service.DeleteEnrollmentAsync(userId, courseId, subjectId);
                    return Results.NoContent();
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

        }
    }
}