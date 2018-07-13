/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 万达Web - 配置层 - Sqlite配置类
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Config
{
    /// <summary>
    /// Sqlite配置
    /// </summary>
    public class SqliteConfig
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connectstr { set; get; }
    }
}
