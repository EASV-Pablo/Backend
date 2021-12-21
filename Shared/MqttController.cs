using Backend.Dtos;
using System;
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
            MqttClient = new MqttClient("ad177c20c01e40b5b108caf50bce4ee4.s1.eu.hivemq.cloud", 8883, true, null, null, MqttSslProtocols.TLSv1_2);

            MqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

            string client = Guid.NewGuid().ToString();

            MqttClient.Connect(client, "backendController", "Pass1234");

            MqttClient.Subscribe(new string[] { "temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            MqttClient.Subscribe(new string[] { "humidity" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            MqttClient.Subscribe(new string[] { "settings" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
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

                case "settings":
                    Console.WriteLine("Settings");
                    break;
            }
        }
    }
}
