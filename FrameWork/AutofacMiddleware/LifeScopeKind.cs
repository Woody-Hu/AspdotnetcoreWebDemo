/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - 生命周期枚举
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMiddleware
{
    /// <summary>
    /// 生命周期枚举
    /// </summary>
    public enum LifeScopeKind
    {
        /// <summary>
        /// 瞬态
        /// </summary>
        Transient,
        /// <summary>
        /// 请求
        /// </summary>
        Request,
        /// <summary>
        /// 单例
        /// </summary>
        Singleton
    }
}
