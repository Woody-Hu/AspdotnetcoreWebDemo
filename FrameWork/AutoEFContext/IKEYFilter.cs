/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 自动化EF上下文框架 - 使用的Key过滤器
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEFContext
{
    /// <summary>
    /// 使用的Key过滤器
    /// </summary>
    public interface IKEYFilter
    {
        /// <summary>
        /// 是否使用相应的Key
        /// </summary>
        /// <param name="inputKey">输入的Key</param>
        /// <returns>是/否</returns>
        bool IfUse(string inputKey);
    }
}
