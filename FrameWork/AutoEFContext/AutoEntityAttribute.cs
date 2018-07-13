/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 自动化EF上下文框架 - 自动Entity特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoEFContext
{
    /// <summary>
    /// 自动Entity特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoEntityAttribute : Attribute
    {
        /// <summary>
        /// 使用的过滤Key
        /// </summary>
        public string Key { set; get; }
    }
}