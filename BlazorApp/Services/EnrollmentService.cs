
using DTOs;
using API.Clients;
using System.Linq;

namespace BlazorApp.Services.Api
{
    public class EnrollmentService : IEnrollmentService
    {
        public async Task<List<UserCourseSubjectDTO>> GetAllAsync() =>
            (await UserCourseSubjectsApiClient.GetAllAsync()).ToList();

        public async Task<List<UserCourseSubjectDTO>> GetByUserAndCourseAsync(int userId, int courseId) =>
            (await UserCourseSubjectsApiClient.GetByUserAndCourseAsync(userId, courseId)).ToList();

        public async Task AddAsync(UserCourseSubjectDTO dto) =>
            await UserCourseSubjectsApiClient.AddAsync(dto);

        public async Task DeleteAsync(int userId, int courseId, int subjectId) =>
            await UserCourseSubjectsApiClient.DeleteAsync(userId, courseId, subjectId);
    }
}