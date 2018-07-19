using AutoEFContextRepository;
using AutofacMiddleware;
using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebDemo.DAO;
using WebDemo.Entity;

namespace WebDemo.Utility
{
    /// <summary>
    /// 缓存公用方法
    /// </summary>
    public static class CacheUtility
    {
        /// <summary>
        /// 使用的特性类型
        /// </summary>
        private static Type m_useType = typeof(CacheKeyAttribute);

        /// <summary>
        /// 获得方法缓存键
        /// </summary>
        /// <param name="inputContext"></param>
        /// <returns></returns>
        public static string GetMethodCacheKey(IInvocationContext inputContext)
        {
            List<int> lstParameterIndex = GetKeyParameters(inputContext.Method);

            if (0 == lstParameterIndex.Count)
            {
                return null;
            }

            return GetMethodCacheKey(inputContext, inputContext.Method, lstParameterIndex);
        }

        /// <summary>
        /// 获得方法缓存键
        /// </summary>
        /// <param name="inputContext">使用的拦截上下文</param>
        /// <param name="tempMethod">使用的方法</param>
        /// <param name="lstParameterIndex">使用的索引列表</param>
        /// <returns></returns>
        public static string GetMethodCacheKey(IInvocationContext inputContext, MethodInfo tempMethod, List<int> lstParameterIndex)
        {
            StringBuilder tempStringBuilder = new StringBuilder();
            tempStringBuilder.Append(tempMethod.DeclaringType.FullName);
            tempStringBuilder.Append(";" + tempMethod.Name);
            foreach (var oneParameterIndex in lstParameterIndex)
            {
                tempStringBuilder.Append(";" + inputContext.Arguments[oneParameterIndex].GetHashCode());
            }

            var tempKey = tempStringBuilder.ToString();
            return tempKey;
        }

        /// <summary>
        /// 获得键参数索引
        /// </summary>
        /// <param name="tempMethod"></param>
        /// <returns></returns>
        public static List<int> GetKeyParameters(MethodInfo tempMethod)
        {
            //参数索引列表
            List<int> lstParameterIndex = new List<int>();
            int tempParameterIndex = 0;

            //拿取属性
            foreach (var oneParameterInfo in tempMethod.GetParameters())
            {
                if (null != oneParameterInfo.GetCustomAttribute(m_useType))
                {
                    lstParameterIndex.Add(tempParameterIndex);
                }

                tempParameterIndex++;
            }

            return lstParameterIndex;
        }

        /// <summary>
        /// 查询缓存
        /// </summary>
        /// <param name="inputKey"></param>
        /// <returns></returns>
        public static CacheEntity GetCache(string inputKey)
        {
            if (string.IsNullOrWhiteSpace(inputKey))
            {
                return null;
            }

            var tempCacheRepository = GetCacheRespository();

            if (null == tempCacheRepository)
            {
                return null;
            }

            var returnValue = tempCacheRepository.Get(k => k.KeyName.Equals(inputKey)).FirstOrDefault();

            return returnValue;
          
        }


        /// <summary>
        /// 增加/变更缓存
        /// </summary>
        /// <param name="inputKey"></param>
        /// <param name="inputValue"></param>
        public static void SetCache(string inputKey,object inputValue)
        {
            if (string.IsNullOrWhiteSpace(inputKey))
            {
                return;
            }

            var tempCacheRepository = GetCacheRespository();

            if (null == tempCacheRepository)
            {
                return;
            }

            var tempValue = tempCacheRepository.Get(k => k.KeyName.Equals(inputKey)).FirstOrDefault();

            if (null != tempValue)
            {
                tempValue.Value = inputValue;
                tempCacheRepository.Update(tempValue);
            }
            else
            {
                tempValue = new CacheEntity();
                tempValue.KeyName = inputKey;
                tempValue.Value = inputValue;

                tempCacheRepository.Add(tempValue);
            }

        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="inputKey"></param>
        public static void DeleteCache(string inputKey)
        {
            if (string.IsNullOrWhiteSpace(inputKey))
            {
                return;
            }

            var tempCacheRepository = GetCacheRespository();

            if (null == tempCacheRepository)
            {
                return;
            }

            var tempValue = tempCacheRepository.Get(k => k.KeyName.Equals(inputKey)).FirstOrDefault();

            if (null != tempValue)
            {
                tempCacheRepository.Delete(tempValue);
            }
        }



        /// <summary>
        /// 获得当前的持久操作接口
        /// </summary>
        /// <returns></returns>
        private static IRespositoryStringKey<MongoDBContext, CacheEntity> GetCacheRespository()
        {
            var tempContext = GolbalAutofacContainer.GetCurrentHttpContext();

            if (null == tempContext)
            {
                return null;
            }

            var tempCacheRepository = tempContext.RequestServices.GetService(typeof(IRespositoryStringKey<MongoDBContext, CacheEntity>)) as IRespositoryStringKey<MongoDBContext, CacheEntity>;

            if (null == tempCacheRepository)
            {
                return null;
            }

            return tempCacheRepository;
        }

    }
}
