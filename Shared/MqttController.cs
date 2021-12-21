using Backend.Dtos;
using System.Text;
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

            MqttClient.Connect("backend", "matciEEAvNNihYdQwp4tsVPeRENvlfuydNylh2KCEnIL9LqtYkNh4qjLUW460ms1", "");

            MqttClient.Subscribe(new string[] { "alarm/gps" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        static void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {            
            switch (e.Topic)
            {
                case "alarm/gps":
                    GPSDto gps = GPSDto.ParseJSONGPSDto(System.Text.Encoding.Default.GetString(e.Message));
                    Program.mongoManager.saveGPSInfo(gps);
                    byte[] bytes = Encoding.ASCII.GetBytes("Apagar alarma");
                    Program.mqttController.MqttClient.Publish("setings/state", bytes);

                    // PUSH NOTIFICATION TO PHONE WITH DATA (IF IS FIRST TIME)

                    break;
            }
        }
    }
}
