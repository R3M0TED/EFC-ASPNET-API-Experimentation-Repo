using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
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

            await Client.PostAsync("/api/Registration", SerializeObject(new UserRegistrationDTO(username, "DisplayName", "Email", "Address", password)));
            var response = await Client.PostAsync("/api/Authentication", SerializeObject(new UserAuthenticationDTO(username, password)));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(responseContent);
            var token = jsonResponse["token"]?.ToString();
            var jsonToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var usernameClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);

            Assert.Multiple(() =>
            {
                Assert.That(token, Is.Not.Null.And.Not.Empty, "Token should be present in response.");
                Assert.That(usernameClaim, Is.Not.Null, "JWT should contain a username claim.");
                Assert.That(usernameClaim?.Value, Is.EqualTo(username), "JWT username claim should match the authenticated user.");
            });
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