using Microsoft.AspNetCore.Mvc;
using ClientConnectorApi.Dtos.Responses;
using ClientConnectorApi.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace ClientConnectorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogonController : BaseController
    {
        private readonly IUserDataService _userDataService;

        public LogonController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUserData()
        {
            return await RunOperationWithExceptionsAsync(async () =>
            {
                return await _userDataService.GetUserDataAsync(User);
            });
        }

    }
}
