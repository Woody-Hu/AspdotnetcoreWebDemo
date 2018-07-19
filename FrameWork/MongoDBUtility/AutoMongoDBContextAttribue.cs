using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MongoDBUtility
{
    /// <summary>
    /// 自动化MongoDB上下文特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false, Inherited = false)]
    public class AutoMongoDBContextAttribue:Attribute
    {
        /// <summary>
        /// 指定的数据库名称
        /// </summary>
        public string UseDataSetName { set; get; }

        /// <summary>
        /// 是否以此类型注册
        /// </summary>
        public bool IfAsThisType { set; get; }

        /// <summary>
        /// 获取使用的数据库名称
        /// </summary>
        /// <param name="inputObj"></param>
        /// <returns></returns>
        public static string GetUseDataSetName(Type inputType)
        {
            var tempType = inputType;

            AutoMongoDBContextAttribue tempAttribute = tempType.GetCustomAttribute(typeof(AutoMongoDBContextAttribue), false) as AutoMongoDBContextAttribue;

            //判定特性
            if (null == tempAttribute || string.IsNullOrWhiteSpace(tempAttribute.UseDataSetName))
            {
                return tempType.Name;
            }
            else
            {
                return tempAttribute.UseDataSetName;
            }
        }

        /// <summary>
        /// 获取是否按此类型注册
        /// </summary>
        /// <param name="inputType"></param>
        /// <returns></returns>
        public static bool GetIfAsThisType(Type inputType)
        {
            var tempType = inputType;
            AutoMongoDBContextAttribue tempAttribute = tempType.GetCustomAttribute(typeof(AutoMongoDBContextAttribue), false) as AutoMongoDBContextAttribue;
            if (null == tempAttribute)
            {
                return false;
            }
            else
            {
                return tempAttribute.IfAsThisType;
            }

        }
    }
}
