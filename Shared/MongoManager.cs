using Backend.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

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

        /**
         * Return the last 'n' GPS signals if the TimeStamp of theese signal are into the last 'timeLapsed' seconds.
         */
        public List<GPSDto> getLastNGPSSignalsInTime(int n, int timeLapsed)
        {
            long timeStamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            long timeFind = timeStamp - timeLapsed;
            var filter = Builders<GPSDto>.Filter.Gte("TimeStamp", timeFind);
            var sort = Builders<GPSDto>.Sort.Descending("TimeStamp");
            var projection = Builders<GPSDto>.Projection.Exclude("_id");
            List<GPSDto> res = DbClient.GetDatabase("bfvubrzyxkoggzi").GetCollection<GPSDto>("GPSInfo").Find(filter).Sort(sort).Limit(n).Project<GPSDto>(projection).ToList();
            return res;
        }
    }
}
