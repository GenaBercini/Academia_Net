
using DTOs;
using API.Clients;
using System.Linq;

namespace BlazorApp.Services.Api
{
    public class SpecialtyService : ISpecialtyService
    {
        public async Task<SpecialtyDTO?> GetAsync(int id) =>
            await SpecialtiesApiClient.GetAsync(id);

        public async Task<List<SpecialtyDTO>> GetAllAsync() =>
            (await SpecialtiesApiClient.GetAllAsync()).ToList();

        public async Task AddAsync(SpecialtyDTO dto) =>
            await SpecialtiesApiClient.AddAsync(dto);

        public async Task UpdateAsync(SpecialtyDTO dto) =>
            await SpecialtiesApiClient.UpdateAsync(dto);

        public async Task DeleteAsync(int id) =>
            await SpecialtiesApiClient.DeleteAsync(id);
    }
}