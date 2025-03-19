using System.Text;
using System.Text.Json;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class BaseTests
    {
        protected HttpClient Client => SetupFixture.Client;

        protected StringContent SerializeObject<T>(T obj)
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }
    }
}