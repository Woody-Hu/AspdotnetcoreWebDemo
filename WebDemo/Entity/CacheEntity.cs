using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Entity
{
    /// <summary>
    /// 缓存Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CacheEntity: DefaultEntity
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string KeyName { set; get; }

        /// <summary>
        /// 缓存值
        /// </summary>
        public object Value { set; get; }
    }
}
