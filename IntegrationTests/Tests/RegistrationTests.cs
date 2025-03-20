using ClientConnectorApi.Dtos.Requests;
using Newtonsoft.Json.Linq;
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
            var displayName = Guid.NewGuid().ToString();
            var email = Guid.NewGuid().ToString();
            var address = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var response = await Client.PostAsync("/api/Registration", SerializeObject(new UserRegistrationDTO(username, displayName, email, address, password)));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);

            Assert.Multiple(() =>
            {
                Assert.That(jsonResponse["username"]?.ToString(), Is.EqualTo(username), "Username should match the registered username.");
                var userData = jsonResponse["userData"] ?? new JObject();
                Assert.That(userData["displayName"]?.ToString(), Is.EqualTo(displayName), "DisplayName should match the registered display name.");
                Assert.That(userData["email"]?.ToString(), Is.EqualTo(email), "Email should match the registered email.");
                Assert.That(userData["address"]?.ToString(), Is.EqualTo(address), "Address should match the registered address.");
            });
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