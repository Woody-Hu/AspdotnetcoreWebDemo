using AutofacMiddleware;
using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDBAutofacMiddlewareImp
{
    /// <summary>
    /// 与Autofac扫描框架的对接机制
    /// </summary>
    public static class AutoMogoDBPreparer
    {
        /// <summary>
        /// 获取准备接口组
        /// </summary>
        /// <returns></returns>
        public static List<IAutofacContainerPrepare> GetPrepares()
        {
            Type useBaseType = typeof(BaseMongoDBContext);

            List<IAutofacContainerPrepare> returnValues = new List<IAutofacContainerPrepare>();

            var allAssembly = AppDomain.CurrentDomain.GetAssemblies();

            //循环程序集扫描
            foreach (var oneAssembly in allAssembly)
            {
                Type[] tempUseTypes = null;

                try
                {
                    tempUseTypes = oneAssembly.GetTypes();
                }
                catch (Exception)
                {
                    continue;
                }

                //循环类型设置
                foreach (var oneType in tempUseTypes)
                {
                    if (!useBaseType.IsAssignableFrom(oneType))
                    {
                        continue;
                    }

                    returnValues.Add(new DefaultAutofacContainerPrepare(oneType, AutoMongoDBContextAttribue.GetIfAsThisType(oneType)));
                }
            }

            return returnValues;
        }
    }
}
