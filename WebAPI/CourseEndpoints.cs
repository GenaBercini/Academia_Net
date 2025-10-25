using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class CourseEndpoints
    {
        public static void MapCourseEndpoints(this WebApplication app)
        {
            app.MapGet("/courses/{id}",async (int id, CourseService courseService) =>
            {
                //CourseService courseService = new CourseService();

                CourseDTO? dto = await courseService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetCourse")
            .Produces<CourseDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/courses",async (CourseService courseService) =>
            {
                //CourseService courseService = new CourseService();
                try 
                {
                    var courses = await courseService.GetAllAsync();
                    return Results.Ok(courses);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

            })
            .WithName("GetAllCourses")
            .Produces<List<CourseDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/courses",async (CourseDTO dto, CourseService courseService) =>
            {
                try
                {
                    //CourseService courseService = new CourseService();

                    CourseDTO courseDTO = await courseService.AddAsync(dto);

                    return Results.Created($"/courses/{courseDTO.Id}", courseDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddCourse")
            .Produces<CourseDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/courses", async (CourseDTO dto, CourseService courseService) =>
            {
                try
                {
                    //CourseService courseService = new CourseService();

                    var found = await courseService.UpdateAsync(dto);

                    if (!found)
                    {
                        return Results.NotFound();
                    }

                    return Results.NoContent();
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("UpdateCourse")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/courses/{id}",async (int id, CourseService courseService) =>
            {
                //CourseService courseService = new CourseService();

                var deleted = await courseService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeleteCourse")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/courses/{courseId:int}/subjects",async (int courseId, CourseService courseService) =>
            {

                try
                {
                    //var courseService = new CourseService();
                    var subjects = await courseService.GetSubjectsByCourse(courseId);
                    return Results.Ok(subjects);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
           .WithName("GetSubjectsByCourse")
           .Produces<IEnumerable<CourseSubjectDTO>>(StatusCodes.Status200OK);

            app.MapPost("/courses/{courseId:int}/subjects",async (int courseId, CourseSubjectDTO dto, CourseService courseService) =>
            {
                try
                {
                    //var courseService = new CourseService();
                    var created = await courseService.AddSubjectToCourse(courseId, dto.SubjectId, dto.DiaHoraDictado);
                    return Results.Created($"/courses/{courseId}/subjects", created);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("AddSubjectToCourse")
            .Produces<CourseSubjectDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}