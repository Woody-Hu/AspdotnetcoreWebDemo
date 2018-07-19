using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 缓存删除特性
    /// </summary>
    public class CacheDeleteAttribute : AbstractInterceptorAttribute
    {
        public override IInvocationInterceptor CreatInterceptor()
        {
            return _CacheDeleteAttribute.m_useInterceptor;
        }

        /// <summary>
        /// 内部类单例模式
        /// </summary>
        private class _CacheDeleteAttribute
        {
            internal static IInvocationInterceptor m_useInterceptor = new CacheDeleteInterceptor();
        }

    }
}
