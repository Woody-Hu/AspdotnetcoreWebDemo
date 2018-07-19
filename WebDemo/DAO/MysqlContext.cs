
using AutoEFContext;
using AutofacEFImp;
using AutofacMiddleware;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Config;
using WebDemo.Utility;

namespace WebDemo.DAO
{
    /// <summary>
    /// MySQL上下文
    /// </summary>
    //[AutoContext]
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
