using Backend.Dtos;
using System;
using System.Text;
using System.Text.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Backend.Shared
{
    public class MqttController
    {
        public MqttClient MqttClient { get; set; }

        public MqttController()
        {
            MqttClient = new MqttClient("mqtt.flespi.io", 1883, false, null, null, MqttSslProtocols.TLSv1_2);

            MqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

            //string client = Guid.NewGuid().ToString();

            MqttClient.Connect("backend", "matciEEAvNNihYdQwp4tsVPeRENvlfuydNylh2KCEnIL9LqtYkNh4qjLUW460ms1", "");

            //MqttClient.Subscribe(new string[] { "temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            //MqttClient.Subscribe(new string[] { "humidity" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            MqttClient.Subscribe(new string[] { "alarm/gps" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        static void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {            
            switch (e.Topic)
            {
                case "temperature":
                    SensorDto sensorInfo = JsonSerializer.Deserialize<SensorDto>(System.Text.Encoding.Default.GetString(e.Message));
                    Program.mongoManager.saveSensorInfo(sensorInfo);
                    Program.socketManager.sendSensorInfo(sensorInfo);
                    break;

                case "humidity":
                    SensorDto sensorInfo2 = JsonSerializer.Deserialize<SensorDto>(System.Text.Encoding.Default.GetString(e.Message));
                    Program.mongoManager.saveSensorInfo(sensorInfo2);
                    Program.socketManager.sendSensorInfo(sensorInfo2);
                    break;

                case "alarm/gps":
                    GPSDto gps = GPSDto.ParseJSONGPSDto(System.Text.Encoding.Default.GetString(e.Message));
                    Program.mongoManager.saveGPSInfo(gps);
                    byte[] bytes = Encoding.ASCII.GetBytes("Apagar alarma");
                    Program.mqttController.MqttClient.Publish("setings/state", bytes);
                    break;
            }
        }
    }
}
