using Backend.Dtos;
using MongoDB.Driver;

namespace Backend.Shared
{
    public class MongoManager
    {
        public MongoClient DbClient { get; set; }
        
        public MongoManager()
        {
            DbClient = new MongoClient(new MongoUrl("mongodb://uipnco8nkijejqlofzci:fy2uBuDm6mRCKR3SqSDD@bfvubrzyxkoggzi-mongodb.services.clever-cloud.com:27017/bfvubrzyxkoggzi"));
        }

        public void saveGPSInfo(GPSDto gps)
        {
            DbClient.GetDatabase("bfvubrzyxkoggzi").GetCollection<GPSDto>("GPSInfo").InsertOne(gps);
        }
    }
}
