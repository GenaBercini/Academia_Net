
using DTOs;
using API.Clients;
using System.Linq;

namespace BlazorApp.Services.Api
{
    public class SubjectService : ISubjectService
    {
        public async Task<SubjectDTO?> GetAsync(int id) =>
            await SubjectsApiClient.GetAsync(id);

        public async Task<List<SubjectDTO>> GetAllAsync() =>
            (await SubjectsApiClient.GetAllAsync()).ToList();

        public async Task<List<SubjectDTO>> GetByCourseIdAsync(int courseId) =>
            (await SubjectsApiClient.GetByCourseIdAsync(courseId)).ToList();

        public async Task AddAsync(SubjectDTO dto) =>
            await SubjectsApiClient.AddAsync(dto);

        public async Task UpdateAsync(SubjectDTO dto) =>
            await SubjectsApiClient.UpdateAsync(dto);

        public async Task DeleteAsync(int id) =>
            await SubjectsApiClient.DeleteAsync(id);
    }
}