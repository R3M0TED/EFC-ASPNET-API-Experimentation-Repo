using Microsoft.AspNetCore.Mvc;
using ClientConnectorApi.Dtos.Responses;
using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Services.Registration;

namespace ClientConnectorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> RegisterUserAsync([FromBody] UserRegistrationDTO registerationDto)
        {
            return await RunOperationWithExceptionsAsync(async () =>
            {
                return await _registrationService.RegisterUserAsync(registerationDto);
            });
        }
    }
}
