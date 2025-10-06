using Ambev.DeveloperEvaluation.Integration.Fixtures;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Base
{
    [Collection("IntegrationTests")]
    public abstract class IntegrationTestBase
    {
        protected readonly HttpClient Client;

        protected IntegrationTestBase(WebApplicationFixture fixture)
        {
            Client = fixture.Client;

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await Client.GetAsync(url);
        }

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T content)
        {
            return await Client.PostAsJsonAsync(url, content);
        }

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T content)
        {
            return await Client.PutAsJsonAsync(url, content);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await Client.DeleteAsync(url);
        }
    }
}