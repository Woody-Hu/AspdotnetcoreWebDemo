﻿/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 自动化EF上下文框架 - EF执行委托封装
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEFContext
{
    /// <summary>
    /// EF执行委托封装
    /// </summary>
    internal class EFDelPacker
    {
        internal OnConfiguringDel UseOnConfig { set; get; }

        internal OnModelCreatingDel UseOnModelCreating { set; get; }
    }
}
