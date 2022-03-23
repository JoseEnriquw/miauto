using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace miauto.identity.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PruebaController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hola mundo");
        }
    }
}
