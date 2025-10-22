using DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class CoursesApiClient : BaseApiClient
    {
        private const string Endpoint = "courses";

        public static async Task<CourseDTO> GetAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"{Endpoint}/{id}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<CourseDTO>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener curso con Id {id}. Detalle: {error}");
        }

        public static async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync(Endpoint);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<IEnumerable<CourseDTO>>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener cursos. Detalle: {error}");
        }

        public static async Task<IEnumerable<CourseSubjectDTO>> GetSubjectsByCourseAsync(int courseId)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"{Endpoint}/{courseId}/subjects");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<IEnumerable<CourseSubjectDTO>>() ?? Array.Empty<CourseSubjectDTO>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener subjects del curso. Detalle: {error}");
        }

        public static async Task<CourseSubjectDTO> AddSubjectToCourseAsync(int courseId, CourseSubjectDTO dto)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync($"{Endpoint}/{courseId}/subjects", dto);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<CourseSubjectDTO>() ?? throw new Exception("Respuesta vacía al crear CourseSubject.");

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al agregar materia al curso. Detalle: {error}");
        }

        public static async Task AddAsync(CourseDTO course)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync(Endpoint, course);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar curso. Detalle: {error}");
            }
        }

        public static async Task UpdateAsync(CourseDTO course)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PutAsJsonAsync(Endpoint, course);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar curso con Id {course.Id}. Detalle: {error}");
            }
        }

        public static async Task DeleteAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"{Endpoint}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar curso con Id {id}. Detalle: {error}");
            }
        }
        public static async Task<IEnumerable<CourseDTO>> GetByCriteriaAsync(string texto)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync($"clientes/criteria?texto={Uri.EscapeDataString(texto)}");

                if (response.IsSuccessStatusCode)
                {
                    var clientes = await response.Content.ReadFromJsonAsync<IEnumerable<CourseDTO>>();
                    return clientes ?? new List<CourseDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al buscar clientes. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al buscar clientes: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al buscar clientes: {ex.Message}", ex);
            }
        }
    }
}
