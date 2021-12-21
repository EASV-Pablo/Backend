using Backend.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Shared
{
    public class MongoManager
    {
        public MongoClient DbClient { get; set; }
        
        public MongoManager()
        {
            DbClient = new MongoClient(new MongoUrl("mongodb://uipnco8nkijejqlofzci:fy2uBuDm6mRCKR3SqSDD@bfvubrzyxkoggzi-mongodb.services.clever-cloud.com:27017/bfvubrzyxkoggzi"));
        }

        public List<SensorDto> getSensorInfo(string sensorId)
        {
            var filter = Builders<SensorDto>.Filter.Eq("SensorId", sensorId);
            var projection = Builders<SensorDto>.Projection.Exclude("_id");
            List<SensorDto> res = DbClient.GetDatabase("bfvubrzyxkoggzi").GetCollection<SensorDto>("SensorInfo").Find(filter).Project<SensorDto>(projection).ToList();
            return res;
        }

        public void saveSensorInfo(SensorDto sensorInfo)
        {
            DbClient.GetDatabase("bfvubrzyxkoggzi").GetCollection<SensorDto>("SensorInfo").InsertOne(sensorInfo);
        }
    }
}
