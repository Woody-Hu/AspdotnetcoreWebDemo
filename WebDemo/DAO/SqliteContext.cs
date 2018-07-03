using AutoEFContext;
using AutofacEFImp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Utility;

namespace WebDemo.DAO
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

            optionsBuilder.UseSqlite("Data Source = Test.db");
        }
    }
}
