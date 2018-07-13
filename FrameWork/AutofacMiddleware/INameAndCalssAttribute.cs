/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac中间件机制框架 - 名称类型特性接口
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacMiddleware
{
    /// <summary>
    /// 名称类型特性接口
    /// </summary>
    public interface INameAndCalssAttribute
    {
        /// <summary>
        /// 是否以类型注册
        /// </summary>
        bool IfByClass { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { set; get; }
    }
}
