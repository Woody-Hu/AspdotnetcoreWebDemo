/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 自动化上下框架Autofac扫描框架对接框架 - 自动化上下文特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutofacEFImp
{
    /// <summary>
    /// 自动化上下文特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoContextAttribute : Attribute
    {
        /// <summary>
        /// 使用的KeyFilter类型
        /// </summary>
        public Type UseKeyFilterType { set; get; }
    }
}