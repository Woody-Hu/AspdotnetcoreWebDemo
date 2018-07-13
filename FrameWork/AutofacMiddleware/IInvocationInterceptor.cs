/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - 方法拦截器接口
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMiddleware
{
    /// <summary>
    /// 方法拦截器接口
    /// </summary>
    public interface IInvocationInterceptor
    {
        /// <summary>
        /// 方法拦截
        /// </summary>
        /// <param name="inputContext"></param>
        void Interceptor(IInvocationContext inputContext);
    }
}
