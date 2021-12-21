using WebSocketSharp;
using Backend.Dtos;
using System.Text.Json;

namespace Backend.Shared
{
    public class WebSocketManager
    {
        WebSocket webSocketClientTemp;
        WebSocket webSocketClientHum;

        public WebSocketManager()
        {
            webSocketClientTemp = new WebSocket("ws://127.0.0.1:7000/Temp");
            webSocketClientTemp.Connect();

            webSocketClientHum = new WebSocket("ws://127.0.0.1:7000/Hum");
            webSocketClientHum.Connect();
        }

        public void sendSensorInfo(SensorDto sensorInfo)
        {
            string message = JsonSerializer.Serialize(sensorInfo);

            if (sensorInfo.SensorId.Contains("temp"))
            {
                webSocketClientTemp.Send(message);
            }

            if (sensorInfo.SensorId.Contains("hum"))
            {
                webSocketClientHum.Send(message);
            }
        }
    }
}
