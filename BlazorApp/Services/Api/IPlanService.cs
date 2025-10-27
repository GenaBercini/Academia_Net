
using DTOs;

namespace BlazorApp.Services.Api
{
    public interface IPlanService
    {
        Task<PlanDTO?> GetAsync(int id);
        Task<List<PlanDTO>> GetAllAsync();
        Task AddAsync(PlanDTO dto);
        Task UpdateAsync(PlanDTO dto);
        Task DeleteAsync(int id);
    }
}