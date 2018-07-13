/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机实现 - HttpContextAccessor准备接口
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using Autofac;
using AutofacMiddleware;
using Microsoft.AspNetCore.Http;
using System;

namespace AutofacMiddlewarePrepare
{
    /// <summary>
    /// HttpContextAccessor准备接口
    /// </summary>
    public class AutofacHttpContextAccessorPrepare : IAutofacContainerPrepare
    {
        public void Prepare(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
        }
    }
}
