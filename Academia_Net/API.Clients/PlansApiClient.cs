using DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class PlansApiClient : BaseApiClient
    {
        private const string Endpoint = "plans";

        public static async Task<PlanDTO> GetAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"{Endpoint}/{id}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<PlanDTO>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener plan con Id {id}. Detalle: {error}");
        }

        public static async Task<IEnumerable<PlanDTO>> GetAllAsync()
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync(Endpoint);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<IEnumerable<PlanDTO>>();

            string error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener planes. Detalle: {error}");
        }

        public static async Task AddAsync(PlanDTO plan)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync(Endpoint, plan);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar plan. Detalle: {error}");
            }
        }

        public static async Task UpdateAsync(PlanDTO plan)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PutAsJsonAsync(Endpoint, plan);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar plan con Id {plan.Id}. Detalle: {error}");
            }
        }

        public static async Task DeleteAsync(int id)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.DeleteAsync($"{Endpoint}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al eliminar plan con Id {id}. Detalle: {error}");
            }
        }
    }
}
