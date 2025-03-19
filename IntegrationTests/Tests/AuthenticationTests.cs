using ClientConnectorApi.Dtos.Requests;
using System.Net;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class AuthenticationTests : BaseTests
    {
        [Test]
        public async Task UserCanAuthenticateWithValidCredentials()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var registrationDto = new UserRegistrationDTO(username, "DisplayName", "Email", "Address", password);
            await Client.PostAsync("/api/Registration", SerializeObject(registrationDto));

            var authenticationDto = new UserAuthenticationDTO(username, password);
            var response = await Client.PostAsync("/api/Authentication", SerializeObject(registrationDto));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task UserCannotAuthenticateWithInvalidCredentials()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var authenticationDto = new UserAuthenticationDTO(username, password);
            var response = await Client.PostAsync("/api/Authentication", SerializeObject(authenticationDto));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}