using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBUtility
{
    /// <summary>
    /// Entity接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }
}
