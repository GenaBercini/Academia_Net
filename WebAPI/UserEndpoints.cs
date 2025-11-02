using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class UsuarioEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/users/{id}", async (int id, UserService userService) =>
            {
                //UserService userService = new UserService();

                UserDTO? user = await userService.GetAsync(id);

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

            app.MapGet("/users", async (UserService userService) =>
            {
                //UserService userService = new UserService();

                var users = await userService.GetAllAsync();

                return Results.Ok(users);
            })
            .WithName("GetAllUsers")
            .Produces<List<UserDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/users",async (UserCreateDTO user, UserService userService) =>
            {
                try
                {
                    //UserService userService = new UserService();

                    UserDTO userDTO = await userService.AddAsync(user);

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

            app.MapPut("/users", async (UserUpdateDTO user, UserService userService) =>
            {
                try
                {
                    //UserService userService = new UserService();

                    var userFound = await userService.UpdateAsync(user);

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

            app.MapPatch("/users/{id}", async (int id, UserService userService) =>
            {
                //UserService userService = new UserService();

                var userDeleted = await userService.DeleteAsync(id);

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

            app.MapPost("/users/{id}/change-password", async (int id, ChangePasswordDTO dto, UserService userService) =>
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.NewPassword) || string.IsNullOrWhiteSpace(dto.CurrentPassword))
                    return Results.BadRequest(new { error = "Debe ingresar la contraseña actual y la nueva." });

                var user = await userService.GetAsync(id);
                if (user == null) return Results.NotFound();

                var changed = await userService.ChangePasswordAsync(id, dto.CurrentPassword, dto.NewPassword);
                if (!changed) return Results.Problem("No se pudo cambiar la contraseña.");
                return Results.NoContent();
            })
            .WithName("ChangeUserPassword")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();


            app.MapGet("/users/report/grades", async (UserService userService, bool onlyStudents = true) =>
            {
                try
                {
                    var pdfBytes = await userService.GenerateUsersGradesReportAsync(onlyStudents);
                    return Results.File(pdfBytes, "application/pdf", "ReporteUsuariosNotas.pdf");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName("GetUsersGradesReport")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithOpenApi();

            app.MapGet("/users/report/advanced", async (UserService userService, bool onlyStudents = true) =>
            {
                try
                {
                    var pdfBytes = await userService.GenerateAdvancedReportAsync(onlyStudents);
                    return Results.File(pdfBytes, "application/pdf", "ReporteAvanzado.pdf");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

        }
    }
}
