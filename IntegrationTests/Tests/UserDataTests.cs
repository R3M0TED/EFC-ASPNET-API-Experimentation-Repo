using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class UserDataTests : BaseTests
    {
        [Test]
        public async Task UserCanRequestUserDataWhenAuthorized()
        {
            var username = Guid.NewGuid().ToString();
            var displayName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            await Client.PostAsync("/api/Registration", SerializeObject(new UserRegistrationDTO(username, displayName, email, address, password)));

            var response = await Client.PostAsync("/api/Authentication", SerializeObject(new UserAuthenticationDTO(username, password)));
            var jwtTokenString = await response.Content.ReadAsStringAsync();
            var tokenDto = JsonSerializer.Deserialize<TokenDTO>(jwtTokenString);

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenDto.token);

            response = await Client.GetAsync("/api/User");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);

            Assert.Multiple(() =>
            {
                Assert.That(jsonResponse["username"]?.ToString(), Is.EqualTo(username), "Username should match the authenticated user's username.");
                var userData = jsonResponse["userData"] ?? new JObject();
                Assert.That(userData["displayName"]?.ToString(), Is.EqualTo(displayName), "DisplayName should match the authenticated user's display name.");
                Assert.That(userData["email"]?.ToString(), Is.EqualTo(email), "Email should match the authenticated user's email.");
                Assert.That(userData["address"]?.ToString(), Is.EqualTo(address), "Address should match the authenticated user's address.");
            });
        }

        [Test]
        public async Task UserCannotRequestUserDataWhenUnauthorized()
        {
            var response = await Client.GetAsync("/api/User");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}