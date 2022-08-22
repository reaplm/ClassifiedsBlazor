using Microsoft.AspNetCore.Mvc;

namespace cb.api.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                return Ok("Service is healthy!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Service is unhealthy");
            }
        }
    }
}
