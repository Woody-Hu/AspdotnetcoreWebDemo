/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - Bean扫描特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMiddleware
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    /// <summary>
    /// Bean扫描特性
    /// </summary>
    public class BeanScanAttribute : Attribute
    {

    }
}
