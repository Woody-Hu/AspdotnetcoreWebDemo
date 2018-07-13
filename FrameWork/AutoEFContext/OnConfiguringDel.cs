/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 自动化EF上下文框架 - 设置委托
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoEFContext
{
    /// <summary>
    /// 设置委托
    /// </summary>
    /// <param name="optionsBuilder"></param>
    public delegate void OnConfiguringDel(DbContextOptionsBuilder optionsBuilder);
}
