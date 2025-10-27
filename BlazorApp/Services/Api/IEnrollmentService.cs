
using DTOs;

namespace BlazorApp.Services.Api
{
    public interface IEnrollmentService
    {
        Task<List<UserCourseSubjectDTO>> GetAllAsync();
        Task<List<UserCourseSubjectDTO>> GetByUserAndCourseAsync(int userId, int courseId);
        Task AddAsync(UserCourseSubjectDTO dto);
        Task DeleteAsync(int userId, int courseId, int subjectId);
    }
}
