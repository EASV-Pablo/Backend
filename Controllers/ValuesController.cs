using Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult TestGet()
        {
            return Content("Hello world Test1");
        }
        
        [HttpPost("last")]
        public string GET_LastNPosition([FromQuery] int number, [FromQuery] int time)
        {
            List<GPSDto> list = Program.mongoManager.getLastNGPSSignalsInTime(number, time);
            return GPSDto.generateJSON(list);
        }        
    }
}
