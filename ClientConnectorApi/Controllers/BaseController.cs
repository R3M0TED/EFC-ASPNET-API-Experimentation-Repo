using ClientConnectorApi.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ClientConnectorApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected async Task<ActionResult<T>> RunOperationWithExceptionsAsync<T>(Func<Task<T>> operation)
        {
            try
            {
                return Ok(await operation());
            }
            catch(UnauthorizedException ex)
            {
                return Unauthorized(new { Status = "Error", Message = ex.Message });

            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Status = "Error", Message = ex.Message });
            }
            catch(DuplicateDataException ex)
            {
                return Conflict(new { Status = "Error", Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = $"An unexpected error occurred. {ex.Message}" });
            }
        }
    }
}
