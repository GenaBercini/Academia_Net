using DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class SubjectsApiClient : BaseApiClient
    {
        private const string Endpoint = "subjects";

        public static async Task<SubjectDTO> GetAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"{Endpoint}/{id}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<SubjectDTO>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener materia con Id {id}. Detalle: {error}");
        }

        public static async Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync(Endpoint);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<IEnumerable<SubjectDTO>>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener materias. Detalle: {error}");
        }

        public static async Task AddAsync(SubjectDTO subject)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync(Endpoint, subject);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar materia. Detalle: {error}");
            }
        }

        public static async Task UpdateAsync(SubjectDTO subject)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PutAsJsonAsync(Endpoint, subject);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar materia con Id {subject.Id}. Detalle: {error}");
            }
        }

        public static async Task DeleteAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"{Endpoint}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar materia con Id {id}. Detalle: {error}");
            }
        }
    }
}
