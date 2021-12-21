using Backend.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Backend
{
    public class Program
    {
        public static MqttController mqttController;
        public static MongoManager mongoManager;

        public static void Main(string[] args)
        {
            mqttController = new MqttController();
            mongoManager = new MongoManager();
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
