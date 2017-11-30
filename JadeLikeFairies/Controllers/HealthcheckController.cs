using Microsoft.AspNetCore.Mvc;

namespace JadeLikeFairies.Controllers
{
    [Route("api/[controller]")]
    public class HealthcheckController : Controller
    {
        // GET api/healthcheck
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
