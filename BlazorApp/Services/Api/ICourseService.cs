
using DTOs;

namespace BlazorApp.Services.Api
{
    public interface ICourseService
    {
        Task<CourseDTO?> GetAsync(int id);
        Task<List<CourseDTO>> GetAllAsync();
        Task<List<CourseSubjectDTO>> GetSubjectsByCourseAsync(int courseId);
        Task<CourseSubjectDTO> AddSubjectToCourseAsync(int courseId, CourseSubjectDTO dto);
        Task AddAsync(CourseDTO dto);
        Task UpdateAsync(CourseDTO dto);
        Task DeleteAsync(int id);
    }
}
