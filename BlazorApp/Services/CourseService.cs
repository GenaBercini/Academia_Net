
using DTOs;
using API.Clients;
using System.Linq;

namespace BlazorApp.Services.Api
{
    public class CourseService : ICourseService
    {
        public async Task<CourseDTO?> GetAsync(int id) =>
            await CoursesApiClient.GetAsync(id);

        public async Task<List<CourseDTO>> GetAllAsync() =>
            (await CoursesApiClient.GetAllAsync()).ToList();

        public async Task<List<CourseSubjectDTO>> GetSubjectsByCourseAsync(int courseId) =>
            (await CoursesApiClient.GetSubjectsByCourseAsync(courseId)).ToList();

        public async Task<CourseSubjectDTO> AddSubjectToCourseAsync(int courseId, CourseSubjectDTO dto) =>
            await CoursesApiClient.AddSubjectToCourseAsync(courseId, dto);

        public async Task AddAsync(CourseDTO dto) =>
            await CoursesApiClient.AddAsync(dto);

        public async Task UpdateAsync(CourseDTO dto) =>
            await CoursesApiClient.UpdateAsync(dto);

        public async Task DeleteAsync(int id) =>
            await CoursesApiClient.DeleteAsync(id);
    }
}