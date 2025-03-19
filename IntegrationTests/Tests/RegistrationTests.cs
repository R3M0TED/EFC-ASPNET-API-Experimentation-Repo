using ClientConnectorApi.Dtos.Requests;
using System.Net;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class RegistrationTests : BaseTests
    {
        [Test]
        public async Task UserCanRegisterWithValidDetails()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var registrationDto = new UserRegistrationDTO(username, "DisplayName", "Email", "Address", password);
            var response = await Client.PostAsync("/api/Registration", SerializeObject(registrationDto));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task UserCannotRegisterWithAnExistingUsername()
        {
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var registrationDto = new UserRegistrationDTO(username, "DisplayName", "Email", "Address", password);
            var response = await Client.PostAsync("/api/Registration", SerializeObject(registrationDto));
            response = await Client.PostAsync("/api/Registration", SerializeObject(registrationDto));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }
    }
}