using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 使用缓存特性
    /// </summary>
    public class CacheAttribute : AbstractInterceptorAttribute
    {
        public override IInvocationInterceptor CreatInterceptor()
        {
            return _CacheAttribute.m_useInterceptor;
        }

        /// <summary>
        /// 内部类单例模式
        /// </summary>
        private class _CacheAttribute
        {
            internal static IInvocationInterceptor m_useInterceptor = new CacheInterceptor();
        }

    }
}
