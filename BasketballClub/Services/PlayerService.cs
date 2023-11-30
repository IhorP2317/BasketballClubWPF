using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Xps;
using BasketballClub.Exceptions;
using BasketballClub.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BasketballClub.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;
   

        public PlayerService(CommonHttpClientService commonHttpClientService)
        {
            _httpClient = commonHttpClientService.HttpClient;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            var response = await _httpClient.GetAsync("players");
            return await HandleResponseAsync<IEnumerable<Player>>(response);
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"players/{id}");
            return await HandleResponseAsync<Player>(response);
        }

        public async Task<Player> CreatePlayerAsync(Player player)
        {
            var response = await _httpClient.PostAsJsonAsync("players", player);
            return await HandleResponseAsync<Player>(response);
        }

        public async Task UpdatePlayerAsync(int id, Player player)
        {
            var response = await _httpClient.PutAsJsonAsync($"players/{id}", player);
            await HandleResponseAsync< HttpResponseMessage>(response);
        }

        public async Task DeletePlayerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"players/{id}");
            await HandleResponseAsync<HttpResponseMessage>(response);
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode) {
                if (response.StatusCode == HttpStatusCode.NotFound) {
                    return default(T);
                }

                var content = await response.Content.ReadAsStringAsync();
                var errorDetails = JsonConvert.DeserializeObject<ApiErrorDetails>(content);
                throw new ApiException(errorDetails.Title, (int)response.StatusCode);
            }

            if (typeof(T) == typeof(HttpResponseMessage)) {
                // When T is HttpResponseMessage, return the response itself
                return (T)(object)response;
            }

            using (var stream = await response.Content.ReadAsStreamAsync()) {
                return await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }
}
