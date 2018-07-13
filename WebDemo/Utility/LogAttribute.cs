/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 万达构件库Web应用-公共方法与公共类- 方法日志拦截器特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
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
