/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// 万达构件库Web应用-公共方法与公共类- 方法日志拦截器
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using AutofacMiddleware;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WanDaWeb.Utility
{
    /// <summary>
    /// 方法日志拦截器
    /// </summary>
    internal class LogerInterceptor : IInvocationInterceptor
    {
        public void Interceptor(IInvocationContext inputContext)
        {
            //获得当前HttpContext
            var tempHttpContext = GolbalAutofacContainer.GetCurrentHttpContext();

            if (null == inputContext)
            {
                inputContext.Proceed();
            }

            var useloger = LogManager.GetLogger(string.Empty, inputContext.InvocationTarget.GetType());

            if (null == useloger)
            {
                inputContext.Proceed();
            }

            var tempIp = tempHttpContext.Connection.LocalIpAddress;

            //执行前后日志与异常日志
            try
            {
                var tempString = string.Format("IP:{0} 调用 类:{1} 方法:{2}", tempIp == null ? "?" : tempIp.ToString(), inputContext.Method.Name,inputContext.TargetType.Name);
                useloger.Log(LogLevel.Info, tempString);
                inputContext.Proceed();
                tempString = string.Format("IP:{0} 调用 类:{1} 方法:{2} 成功", tempIp == null ? "?" : tempIp.ToString(), inputContext.Method.Name, inputContext.TargetType.Name);
                useloger.Log(LogLevel.Info, tempString);
            }
            catch (Exception ex)
            {
                var tempString = string.Format("IP:{0} 调用 类:{1} 方法:{2} 出现异常{3}", tempIp == null ? "?" : tempIp.ToString(),inputContext.TargetType.Name ,inputContext.Method.Name,ex.Message);
                useloger.Log(LogLevel.Error, tempString);
                throw;
            }


        }
    }
}
