using DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class SpecialtiesApiClient : BaseApiClient
    {
        private const string Endpoint = "specialties";

        public static async Task<SpecialtyDTO> GetAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"{Endpoint}/{id}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<SpecialtyDTO>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener especialidad con Id {id}. Detalle: {error}");
        }

        public static async Task<IEnumerable<SpecialtyDTO>> GetAllAsync()
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync(Endpoint);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<IEnumerable<SpecialtyDTO>>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener especialidades. Detalle: {error}");
        }

        public static async Task AddAsync(SpecialtyDTO specialty)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync(Endpoint, specialty);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar especialidad. Detalle: {error}");
            }
        }

        public static async Task UpdateAsync(SpecialtyDTO specialty)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PutAsJsonAsync(Endpoint, specialty);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar especialidad con Id {specialty.Id}. Detalle: {error}");
            }
        }

        public static async Task DeleteAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"{Endpoint}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar especialidad con Id {id}. Detalle: {error}");
            }
        }
    }
}
