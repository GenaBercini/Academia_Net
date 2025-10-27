
using DTOs;
using API.Clients;
using System.Linq;

namespace BlazorApp.Services.Api
{
    public class PlanService : IPlanService
    {
        public async Task<PlanDTO?> GetAsync(int id) =>
            await PlansApiClient.GetAsync(id);

        public async Task<List<PlanDTO>> GetAllAsync() =>
            (await PlansApiClient.GetAllAsync()).ToList();

        public async Task AddAsync(PlanDTO dto) =>
            await PlansApiClient.AddAsync(dto);

        public async Task UpdateAsync(PlanDTO dto) =>
            await PlansApiClient.UpdateAsync(dto);

        public async Task DeleteAsync(int id) =>
            await PlansApiClient.DeleteAsync(id);
    }
}