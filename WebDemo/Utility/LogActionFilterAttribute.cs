/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 万达构件库Web应用-公共方法与公共类- Control日志拦截器特性
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
{
    /// <summary>
    /// Control日志拦截器特性
    /// </summary>
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly Logger m_useLogger = null;

        public LogActionFilterAttribute()
        {
            m_useLogger = LogManager.GetLogger(string.Empty);
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tempHttpContext = context.HttpContext;

            string useString = string.Format("IP:{0} 访问:{1}", tempHttpContext.Connection.LocalIpAddress.ToString(), tempHttpContext.Request.Path);

            m_useLogger.Log(LogLevel.Info, useString);
        }
    }
}
