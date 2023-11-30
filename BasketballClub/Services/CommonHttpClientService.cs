using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BasketballClub.Exceptions;

namespace BasketballClub.Services {
    public  class CommonHttpClientService  {
        public HttpClient HttpClient { get; }

        public CommonHttpClientService() {
            HttpClient = new HttpClient {
                BaseAddress = new Uri("https://localhost:7274/api/")
            };

            SetupHttpClient();
        }

        private void SetupHttpClient() {
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/problem+json"));

            var jsonConverter = new ErrorHandlingJsonConverter();

            // Configure other headers if needed
            HttpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            HttpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("ISO-8859-1"));

            // Add the ErrorHandlingJsonConverter to the HttpClient's serializer
            var jsonSerializerSettings = new JsonSerializerSettings {
                Converters = { jsonConverter }
            };

            // Apply JsonSerializerSettings to handle errors in responses
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/problem+json"));
            HttpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            HttpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("ISO-8859-1"));

            // Add other configurations as needed
        }
    }
}
