
using DTOs;

namespace BlazorApp.Services.Api
{
    public interface ISpecialtyService
    {
        Task<SpecialtyDTO?> GetAsync(int id);
        Task<List<SpecialtyDTO>> GetAllAsync();
        Task AddAsync(SpecialtyDTO dto);
        Task UpdateAsync(SpecialtyDTO dto);
        Task DeleteAsync(int id);
    }
}