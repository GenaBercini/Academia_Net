using Application.Services;
using DTOs;

namespace WebAPI
{
    public static class PlanEndpoints
    {
        public static void MapPlanEndpoints(this WebApplication app)
        {
            app.MapGet("/plans/{id}", (int id) =>
            {
                PlanService planService = new PlanService();

                PlanDTO? dto = planService.Get(id);

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

            app.MapGet("/plans", () =>
            {
                PlanService planService = new PlanService();

                var dtos = planService.GetAll();

                return Results.Ok(dtos);
            })
            .WithName("GetAllPlans")
            .Produces<List<PlanDTO>>(StatusCodes.Status200OK)
            .WithOpenApi();

            app.MapPost("/plans", (PlanDTO dto) =>
            {
                try
                {
                    PlanService planService = new PlanService();

                    PlanDTO planDTO = planService.Add(dto);

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

            app.MapPut("/plans", (PlanDTO dto) =>
            {
                try
                {
                    PlanService planService = new PlanService();

                    var found = planService.Update(dto);

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

            app.MapDelete("/plans/{id}", (int id) =>
            {
                PlanService planService = new PlanService();

                var deleted = planService.Delete(id);

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

