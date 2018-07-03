using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MongoDBUtility
{
    /// <summary>
    /// 使用的MongoDBEntity特性
    /// </summary>
    public class MongoDBEntityAttribute:Attribute
    {
        /// <summary>
        /// 使用的名字
        /// </summary>
        public string UseName { set; get; }

        /// <summary>
        /// 获取使用的名字
        /// </summary>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static string GetUseName(Type inputType)
        {
            MongoDBEntityAttribute tempAttribute = inputType.GetCustomAttribute(inputType, false) as MongoDBEntityAttribute;

            if (null == tempAttribute || string.IsNullOrWhiteSpace(tempAttribute.UseName))
            {
                return inputType.Name;
            }
            else
            {
                return tempAttribute.UseName;
            }
        }
    }
}
