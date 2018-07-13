/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac扫描注入框架 - IOC - 通用的Autofac模块
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using AutofacMiddleware;

namespace AutofacUtility
{
    /// <summary>
    /// 通用的Autofac模块
    /// </summary>
    public class AutofacModule : Autofac.Module
    {
        private List<IAutofacContainerPrepare> m_lstUseMiddleware = null;

        public AutofacModule(List<IAutofacContainerPrepare> inputLst)
        {
            if (null != inputLst)
            {
                m_lstUseMiddleware = new List<IAutofacContainerPrepare>();

                m_lstUseMiddleware.AddRange(inputLst);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            //获取临时module应用
            var tempAutoApplication = AutofacApplication.PrepareApplication(builder);

            //调用传入委托
            if (null != m_lstUseMiddleware)
            {
                foreach (var oneMiddleware in m_lstUseMiddleware)
                {
                    if (null != oneMiddleware)
                    {
                        oneMiddleware.Prepare(builder);
                    }
                }
            }
        }
    }
}
