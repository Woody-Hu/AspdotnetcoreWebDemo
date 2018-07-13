/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - 抽象拦截器特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutofacMiddleware
{
    /// <summary>
    /// 抽象拦截器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public abstract class AbstractInterceptorAttribute : Attribute
    {
        public abstract IInvocationInterceptor CreatInterceptor();
    }
}