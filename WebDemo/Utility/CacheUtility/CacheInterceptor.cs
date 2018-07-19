using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebDemo.Entity;

namespace WebDemo.Utility
{
    /// <summary>
    /// 缓存拦截器
    /// </summary>
    public class CacheInterceptor : IInvocationInterceptor
    {
        /// <summary>
        /// 拦截机制
        /// </summary>
        /// <param name="inputContext"></param>
        public void Interceptor(IInvocationContext inputContext)
        {
            //获取方法
            var tempMethod = inputContext.Method;

            //获得缓存键
            var tempKey = CacheUtility.GetMethodCacheKey(inputContext);

            if (string.IsNullOrWhiteSpace(tempKey))
            {
                inputContext.Proceed();
                return;
            }
            else
            {
                //是否已执行
                bool ifProcessed = false;

                try
                {
                    var tempCache = CacheUtility.GetCache(tempKey);

                    //若没有缓存
                    if (null == tempCache || tempCache.GetType() != tempMethod.ReturnType)
                    {
                        //设置执行状态
                        ifProcessed = true;
                        inputContext.Proceed();

                        //保存缓存
                        CacheUtility.SetCache(tempKey, inputContext.ReturnValue);
                    }
                    else
                    {
                        //设置缓存
                        inputContext.ReturnValue = tempCache.Value;
                    }

                }
                //异常直接执行
                catch (Exception)
                {
                    //若未执行
                    if (!ifProcessed)
                    {
                        inputContext.Proceed();
                    }
                    return;
                }


            }
        }
    }
}
