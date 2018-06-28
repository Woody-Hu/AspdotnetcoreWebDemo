using Microsoft.AspNetCore.Mvc.Filters;

using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 日志方法拦截器
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
