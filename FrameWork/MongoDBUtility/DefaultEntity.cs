using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBUtility
{
    /// <summary>
    /// 默认的string主键Entity实现
    /// </summary>
    [BsonIgnoreExtraElements(Inherited = true)]
    public class DefaultEntity : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

   
    }
}
