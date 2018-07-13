/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - Bean特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMiddleware
{
    /// <summary>
    /// Bean特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BeanAttribute : Attribute, INameAndCalssAttribute
    {
        /// <summary>
        /// 是否以类型注册
        /// </summary>
        public bool IfByClass { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
    }
}
