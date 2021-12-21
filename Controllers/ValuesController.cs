using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult TestGet()
        {
            return Content("Hola Test1");
        }
    }
}
