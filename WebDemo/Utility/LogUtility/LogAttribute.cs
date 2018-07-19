
using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 方法日志拦截器特性
    /// </summary>
    public class LogAttribute : AbstractInterceptorAttribute
    {
        public override IInvocationInterceptor CreatInterceptor()
        {
            return _LogAttribute.m_useInterceptro;
        }

        /// <summary>
        /// 内部类实现线程安全饿汉单例模式
        /// </summary>
        private class _LogAttribute
        {
            internal static IInvocationInterceptor m_useInterceptro = new LogerInterceptor();
        }

    }
}
