using DTOs;

namespace BlazorApp.Services.Api
{
    public interface ISubjectService
    {
        Task<SubjectDTO?> GetAsync(int id);
        Task<List<SubjectDTO>> GetAllAsync();
        Task<List<SubjectDTO>> GetByCourseIdAsync(int courseId);
        Task AddAsync(SubjectDTO dto);
        Task UpdateAsync(SubjectDTO dto);
        Task DeleteAsync(int id);
    }
}