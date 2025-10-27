using DTOs;

namespace BlazorApp.Services.Api
{
    public interface IUserService
    {
        Task<UserDTO?> GetAsync(int id);
        Task<List<UserDTO>> GetAllAsync();
        Task AddAsync(UserCreateDTO dto);
        Task UpdateAsync(UserUpdateDTO dto);
        Task DeleteAsync(int id);

        Task<bool> EnrollAsync(int userId, UserCourseSubjectDTO enrollment);
        Task<List<UserCourseSubjectDTO>> GetEnrollmentsAsync(int userId);
        Task<byte[]> GetUsersGradesReportAsync(bool onlyStudents = true);
    }
}