using System.Net;
using System.Text.Json;
using TradingBook.ExternalServices.Http.Abstract;

namespace TradingBook.ExternalServices.Http.Implementation
{
    internal class HttpService : IHttpService
    {
        private JsonSerializerOptions JsonConfiguration
        {
            get
            {
                return new JsonSerializerOptions(JsonSerializerDefaults.General);
            }
        }

        public async Task<T> Get<T>(Uri uri, IReadOnlyDictionary<string, string> parameters = null, IReadOnlyDictionary<string, string> headers = null)
        {
            using HttpClient client = new HttpClient();

            if (headers != null)
            {
                foreach (var headerAux in headers)
                {
                    client.DefaultRequestHeaders.Add(headerAux.Key, headerAux.Value);
                }
            }

            string queryParameter = String.Empty;

            if(parameters != null)
            {
                List<string> queryParametersCollection = new List<string>();

                foreach (var parameterAux in parameters)
                {   
                    queryParametersCollection.Add($"{parameterAux.Key}={parameterAux.Value}");
                }

                if (queryParametersCollection.Count > 0)
                {
                    queryParameter = $"?{String.Join('&', queryParametersCollection)}";
                }
            }

            string url = $"{uri.ToString()}{queryParameter}";

            HttpResponseMessage response = await client.GetAsync(url) ?? throw new HttpRequestException();

            T deserializedResponse = await DeserializeResponseAsync<T>(response);

            return deserializedResponse;
        }

        private async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException();

                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.PreconditionFailed:
                        string errorContent = await response.Content.ReadAsStringAsync() ?? String.Empty;
                        throw new InvalidOperationException($"{response.ReasonPhrase}:{errorContent}");

                    default:
                        string exceptionMsg = await response.Content.ReadAsStringAsync() ?? String.Empty;
                        throw new Exception($"{response.ReasonPhrase}:{exceptionMsg}");
                }
            }

            string jsonContent = await response.Content.ReadAsStringAsync() ?? String.Empty;

            T responseDeserialized = JsonSerializer.Deserialize<T>(jsonContent, JsonConfiguration) ?? throw new NullReferenceException(nameof(T));

            return responseDeserialized;
        }
    }
}
