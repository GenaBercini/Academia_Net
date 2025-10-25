using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PlanEndpoints
    {
        public static void MapPlanEndpoints(this WebApplication app)
        {
            app.MapGet("/plans/{id}", async (int id)  =>
            {
                PlanService planService = new PlanService();

                PlanDTO? dto = await planService.GetAsync(id);

                if (dto == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(dto);
            })
            .WithName("GetPlan")
            .Produces<PlanDTO>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

            app.MapGet("/plans", async () =>
            {
                PlanService planService = new PlanService();

                var dtos = await planService.GetAllAsync();

                return Results.Ok(dtos);
            })
            .WithName("GetAllPlans")
            .Produces<List<PlanDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/plans", async (PlanDTO dto) =>
            {
                try
                {
                    PlanService planService = new PlanService();

                    PlanDTO planDTO = await planService.AddAsync(dto);

                    return Results.Created($"/plans/{planDTO.Id}", planDTO);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithName("AddPlan")
            .Produces<PlanDTO>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapPut("/plans", async (PlanDTO dto) =>
            {
                try
                {
                    PlanService planService = new PlanService();

                    var found = await planService.UpdateAsync(dto);

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
            .WithName("UpdatePlan")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi();

            app.MapDelete("/plans/{id}", async (int id) =>
            {
                PlanService planService = new PlanService();

                var deleted = await planService.DeleteAsync(id);

                if (!deleted)
                {
                    return Results.NotFound();
                }

                return Results.NoContent();
            })
            .WithName("DeletePlan")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}

