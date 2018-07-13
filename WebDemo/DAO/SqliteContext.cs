/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 万达构件库Web应用- DAO层 - Sqlite数据库上下文
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using AutoEFContext;
using AutofacEFImp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanDaWeb.Config;
using WanDaWeb.Utility;

namespace WanDaWeb.DAO
{
    [AutoContext]
    /// <summary>
    /// 使用的Sqlite数据库上下文
    /// </summary>
    public class SqliteContext:AutoContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //获取配置
            SqliteConfig useConfig = ConfigPacker.GetConfigPacker().GetConfig<SqliteConfig>();

            optionsBuilder.UseSqlite(useConfig.Connectstr);
        }
    }
}
