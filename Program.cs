using Backend.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Backend
{
    public class Program
    {
        public static MqttController mqttController;
        public static MongoManager mongoManager;
        public static WebSocketManager socketManager;

        public static void Main(string[] args)
        {
            mqttController = new MqttController();
            mongoManager = new MongoManager();
            //socketManager = new WebSocketManager();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
