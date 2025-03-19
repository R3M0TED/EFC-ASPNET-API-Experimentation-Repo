using Microsoft.AspNetCore.Mvc;
using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Services.Authentication;
using ClientConnectorApi.Dtos.Responses;

namespace ClientConnectorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
        public async Task<ActionResult<TokenDTO>> AuthenticateAsync([FromBody] UserAuthenticationDTO authenticationDto)
        {
            return await RunOperationWithExceptionsAsync(async () =>
            {
                return await _authenticationService.AuthenticateAsync(authenticationDto);
            });
        }
    }
}
