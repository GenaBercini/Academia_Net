using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/users/{id}", (int id) =>
            {
                UserService userService = new UserService();

                UserDTO? user = userService.Get(id);

                if (user == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(user);
            })
                .WithName("GetUsers")
                .Produces<UserDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi();

            app.MapGet("/users", () =>
            {
                UserService userService = new UserService();

                var users = userService.GetAll();

                return Results.Ok(users);
            })
            .WithName("GetAllUsers")
            .Produces<List<UserDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/users", (UserCreateDTO user) =>
            {
                try
                {
                    UserService userService = new UserService();

                    UserDTO userDTO = userService.Add(user);

                    return Results.Created($"/clientes/{userDTO.Id}", userDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddUser")
            .Produces<UserDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/users", (UserUpdateDTO user) =>
            {
                try
                {
                    UserService userService = new UserService();

                    var userFound = userService.Update(user);

                    if (!userFound)
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
            .WithName("UpdateUser")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPatch("/users/{id}", (int id) =>
            {
                UserService userService = new UserService();

                var userDeleted = userService.Delete(id);

                if (!userDeleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();

            })
            .WithName("DeleteUser")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapPost("/users/{userId:int}/enroll", (int userId, UserCourseSubjectCreateDTO dto) =>
            {
                EnrollmentService enrollmentService = new EnrollmentService();
                try
                {
                    bool created = enrollmentService.EnrollUserInCourseSubject(userId, dto.CourseId, dto.SubjectId);
                    if (!created)
                        return Results.Conflict(new { Message = "El usuario ya está inscripto en esa materia." });

                    return Results.Created($"/users/{userId}/enrollments", null);
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
            .WithName("EnrollUser")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict);

            app.MapGet("/users/{userId:int}/enrollments", (int userId) =>
            {
                EnrollmentService enrollmentService = new EnrollmentService();
                try
                {
                    var enrollments = enrollmentService.GetEnrollmentsByUser(userId);
                    return Results.Ok(enrollments);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("GetUserEnrollments")
            .Produces<IEnumerable<UserCourseSubjectDTO>>(StatusCodes.Status200OK);
        }
    }
}
