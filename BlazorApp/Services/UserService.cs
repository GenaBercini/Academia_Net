using DTOs;
using API.Clients;
using System.Linq;

namespace BlazorApp.Services.Api
{
    public class UserService : IUserService
    {
        public async Task<UserDTO?> GetAsync(int id) =>
            await UsersApiClient.GetAsync(id);

        public async Task<List<UserDTO>> GetAllAsync() =>
            (await UsersApiClient.GetAllAsync()).ToList();

        public async Task AddAsync(UserCreateDTO dto) =>
            await UsersApiClient.AddAsync(dto);

        public async Task UpdateAsync(UserUpdateDTO dto) =>
            await UsersApiClient.UpdateAsync(dto);

        public async Task DeleteAsync(int id) =>
            await UsersApiClient.DeleteAsync(id);

        public async Task<bool> EnrollAsync(int userId, UserCourseSubjectDTO enrollment) =>
            await UsersApiClient.EnrollAsync(userId, enrollment);

        public async Task<List<UserCourseSubjectDTO>> GetEnrollmentsAsync(int userId) =>
            (await UsersApiClient.GetEnrollmentsAsync(userId)).ToList();

        public async Task<byte[]> GetUsersGradesReportAsync(bool onlyStudents = true) =>
            await UsersApiClient.GetUsersGradesReportAsync(onlyStudents);
    }
}