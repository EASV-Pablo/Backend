using Backend.Dtos;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Backend.Shared
{
    public class MqttController
    {
        public MqttClient MqttClient { get; set; }

        private static long lastNotification = 0;
        private static int thresholdTime = 60;

        public MqttController()
        {
            MqttClient = new MqttClient("mqtt.flespi.io", 1883, false, null, null, MqttSslProtocols.TLSv1_2);

            MqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

            MqttClient.Connect("backendTEST", "IPLpVNmWlgceZThcQirxxJPiv5U0erRK7sXS5aCegCj5jo5ZwmmTUZ2dVK3X2IwX", "");

            MqttClient.Subscribe(new string[] { "alarm/gps" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        static void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            switch (e.Topic)
            {
                case "alarm/gps":
                    GPSDto gps = GPSDto.ParseJSONGPSDto(System.Text.Encoding.Default.GetString(e.Message));
                    Program.mongoManager.saveGPSInfo(gps);
                    // byte[] bytes = Encoding.ASCII.GetBytes("Apagar alarma");
                    // Program.mqttController.MqttClient.Publish("setings/state", bytes);
                    /*
                    if ( (gps.TimeStamp - lastNotification) > thresholdTime)
                    {
                        lastNotification = gps.TimeStamp;
                        // Lanzar push
                        FirebaseController fbc = new FirebaseController();
                        Dictionary<string, string> d = new Dictionary<string, string>();
                        d.Add("latitude", gps.Latitude.ToString());
                        d.Add("longitude", gps.Longitude.ToString());
                        fbc.setMessage(d,"Titulo de la notificacion","Cuerpo de la notificacion");
                        fbc.sendNotification();
                    } */

                    break;
            }
        }
    }
}
