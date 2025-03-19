using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;
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
            var password = Guid.NewGuid().ToString();

            var registrationDto = new UserRegistrationDTO(username, "DisplayName", "Email", "Address", password);
            await Client.PostAsync("/api/Registration", SerializeObject(registrationDto));

            var authenticationDto = new UserAuthenticationDTO(username, password);
            var response = await Client.PostAsync("/api/Authentication", SerializeObject(authenticationDto));
            var jwtTokenString = await response.Content.ReadAsStringAsync();
            var tokenDto = JsonSerializer.Deserialize<TokenDTO>(jwtTokenString);

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenDto.token);

            response = await Client.GetAsync("/api/User");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task UserCannotRequestUserDataWhenUnauthorized()
        {
            var response = await Client.GetAsync("/api/User");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}