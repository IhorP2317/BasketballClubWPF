using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BasketballClub.Exceptions;
using BasketballClub.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BasketballClub.Services
{
    class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;

        public TeamService(CommonHttpClientService commonHttpClientService) {
            _httpClient = commonHttpClientService.HttpClient;
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            var responce = await _httpClient.GetAsync("teams");
            return await HandleResponseAsync<IEnumerable<Team>>(responce);

        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            var responce = await _httpClient.GetAsync($"teams/{id}");
            return await HandleResponseAsync<Team>(responce);
        }


        public async Task<Team> CreateTeamAsync(Team team)
        {
            var responce = await _httpClient.PostAsJsonAsync("teams", team);
            return await HandleResponseAsync<Team>(responce);
        }

        public async Task UpdateTeamAsync(int id, Team team)
        {

            var responce = await _httpClient.PutAsJsonAsync($"teams/{id}", team); 
            await HandleResponseAsync<HttpResponseMessage>(responce);
        }

        public async Task DeleteTeamAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"teams/{id}");
            await HandleResponseAsync<HttpResponseMessage>(response);
        }
        public async Task<bool> IsTeamExistAsync(int id) {
            var response = await _httpClient.GetAsync($"teams/{id}");

            if (response.IsSuccessStatusCode) {
                // Team with the specified ID exists
                return true;
            }

            if (response.StatusCode == HttpStatusCode.NotFound) {
                // Team with the specified ID does not exist
                return false;
            }

            // Handle other cases or throw an exception if needed
            var content = await response.Content.ReadAsStringAsync();
            var errorDetails = JsonConvert.DeserializeObject<ApiErrorDetails>(content);
            throw new ApiException(errorDetails.Title, (int)response.StatusCode);
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return default(T);
                }

                var content = await response.Content.ReadAsStringAsync();
                var errorDetails = JsonConvert.DeserializeObject<ApiErrorDetails>(content);
                throw new ApiException(errorDetails.Title, (int)response.StatusCode);
            }

            if (typeof(T) == typeof(HttpResponseMessage))
            {
                // When T is HttpResponseMessage, return the response itself
                return (T)(object)response;
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}
