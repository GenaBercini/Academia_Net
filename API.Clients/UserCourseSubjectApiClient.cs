using DTOs;
using System.Net.Http.Json;

namespace API.Clients
{
    public class UserCourseSubjectsApiClient : BaseApiClient
    {
        private const string Endpoint = "userCourseSubjects";

        public static async Task<IEnumerable<UserCourseSubjectDTO>> GetAllAsync()
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync(Endpoint);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserCourseSubjectDTO>>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener inscripciones. Detalle: {error}");
        }

        public static async Task<IEnumerable<UserCourseSubjectDTO>> GetByUserAndCourseAsync(int userId, int courseId)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"{Endpoint}?userId={userId}&courseId={courseId}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserCourseSubjectDTO>>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener inscripciones del usuario {userId} en curso {courseId}. Detalle: {error}");
        }

        public static async Task AddAsync(UserCourseSubjectDTO enrollment)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync(Endpoint, enrollment);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar inscripción. Detalle: {error}");
            }
        }

        public static async Task DeleteAsync(int userId, int courseId, int subjectId)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"{Endpoint}?userId={userId}&courseId={courseId}&subjectId={subjectId}");

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar inscripción. Detalle: {error}");
            }
        }
    }
}
