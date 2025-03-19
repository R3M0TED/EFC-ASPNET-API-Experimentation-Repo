using ClientConnectorApi.Dtos.Requests;
using System.Net;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class UserDataTests : BaseTests
    {
        [Test]
        public async Task UserCanRequestUserData()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var registrationDto = new UserRegistrationDTO(username, "DisplayName", "Email", "Address", password);
            await Client.PostAsync("/api/Registration", ConvertToJson(registrationDto));

            var authenticationDto = new UserAuthenticationDTO(username, password);
            var response = await Client.PostAsync("/api/Authentication", ConvertToJson(registrationDto));
            var jwtToken = await response.Content.ReadAsStringAsync();

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            response = await Client.GetAsync("/api/User");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}