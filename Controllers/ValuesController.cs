using Backend.Dtos;
using Backend.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet("temperature/")]
        public SensorDto[] TemperatureGetAll()
        {            
            List<SensorDto> list = Program.mongoManager.getSensorInfo("tempSensor1");
            SensorDto[] sensor = list.ToArray();
            return sensor;
        }

        [HttpGet("humidity/")]
        public SensorDto[] HumidityGetAll()
        {
            List<SensorDto> list = Program.mongoManager.getSensorInfo("humSensor1");
            SensorDto[] sensor = list.ToArray();
            return sensor;
        }

        [HttpGet("settings/")]
        public SettingsDto[] SettingsGetAll()
        {
            SettingsDto[] sensor = new SettingsDto[] { };
            return sensor;
        }

        [HttpGet("test")]
        public IActionResult TestGet()
        {
            return Content("Hola Test1");
        }
    }
}
