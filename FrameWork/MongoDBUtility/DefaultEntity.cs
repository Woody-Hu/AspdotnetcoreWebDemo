/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// MongoDB操作框架 -  默认的string主键Entity实现
// 创建标识：胡迪 2018.07.04
//----------------------------------------------------------------*/
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
