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
        }
    }
}
