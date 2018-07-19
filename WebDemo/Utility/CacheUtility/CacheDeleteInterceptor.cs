using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 缓存删除拦截器
    /// </summary>
    public class CacheDeleteInterceptor : IInvocationInterceptor
    {
        public void Interceptor(IInvocationContext inputContext)
        {
            //获得缓存键
            var tempKey = CacheUtility.GetMethodCacheKey(inputContext);

            inputContext.Proceed();

            if (!string.IsNullOrEmpty(tempKey))
            {
                CacheUtility.DeleteCache(tempKey);
            }
        }
    }
}
