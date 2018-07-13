/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - Autofac中间操作接口
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using Autofac;
using System;

namespace AutofacMiddleware
{
    /// <summary>
    /// Autofac中间操作接口
    /// </summary>
    public interface IAutofacContainerPrepare
    {
        void Prepare(ContainerBuilder builder);
    }
}
