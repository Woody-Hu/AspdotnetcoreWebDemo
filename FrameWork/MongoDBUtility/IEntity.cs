/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// MongoDB操作框架 - Entity接口
// 创建标识：胡迪 2018.07.04
//----------------------------------------------------------------*/

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
