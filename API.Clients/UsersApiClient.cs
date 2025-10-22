using DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
{
    public class UsersApiClient : BaseApiClient
    {
        public static async Task<UserDTO> GetAsync(int id)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync("users/" + id);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<UserDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener usuario con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener usuario con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener usuario con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.GetAsync("users");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<UserDTO>>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener lista de usuarios. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener lista de usuarios: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener lista de usuarios: {ex.Message}", ex);
            }
        }

        public async static Task AddAsync(UserCreateDTO users)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PostAsJsonAsync("users", users);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear usuario. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al crear usuario: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al crear usuario: {ex.Message}", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PatchAsync($"users/{id}", null);
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar usuario con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar usuario con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar usuario con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task UpdateAsync(UserUpdateDTO user)
        {
            try
            {
                using var client = await CreateHttpClientAsync();
                HttpResponseMessage response = await client.PutAsJsonAsync("users", user);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar usuario con Id {user.Id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al actualizar usuario con Id {user.Id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al actualizar usuario con Id {user.Id}: {ex.Message}", ex);
            }
        }

        public static async Task<bool> EnrollAsync(int userId, UserCourseSubjectCreateDTO enrollment)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.PostAsJsonAsync($"users/{userId}/enroll", enrollment);

            if (response.IsSuccessStatusCode)
                return true;

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                return false;

            string err = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al inscribir usuario: {err}");
        }

        public static async Task<IEnumerable<UserCourseSubjectDTO>> GetEnrollmentsAsync(int userId)
        {
            using var client = await CreateHttpClientAsync();
            var response = await client.GetAsync($"users/{userId}/enrollments");

            if (response.IsSuccessStatusCode)
            {
                var dto = await response.Content.ReadFromJsonAsync<IEnumerable<UserCourseSubjectDTO>>();
                return dto ?? Array.Empty<UserCourseSubjectDTO>();
            }

            string err = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener inscripciones: {err}");
        }
    }
}
