/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 万达构件库Web应用- DAO层 - MySQL上下文
// 创建标识：董淑珍 2018.07.03
//----------------------------------------------------------------*/
using AutoEFContext;
using AutofacEFImp;
using AutofacMiddleware;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanDaWeb.Config;
using WanDaWeb.Utility;

namespace WanDaWeb.DAO
{
    /// <summary>
    /// MySQL上下文
    /// </summary>
    [AutoContext]
    public class MysqlContext: AutoContext
    {

        /// <summary>
        /// 重写的OnConfiguring方法
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            MySqlConfig useConfig = ConfigPacker.GetConfigPacker().GetConfig<MySqlConfig>();

            optionsBuilder.UseMySql(useConfig.Connectstr);

        }
    }
}
