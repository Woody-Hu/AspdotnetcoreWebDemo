﻿/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - 属性注入特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMiddleware
{
    /// <summary>
    /// 属性注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DenpencyAttribute : Attribute
    {
        /// <summary>
        /// 使用的名称
        /// </summary>
        public string Name { set; get; }
    }
}
