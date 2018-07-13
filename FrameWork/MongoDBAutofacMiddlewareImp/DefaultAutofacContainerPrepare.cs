/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// MongoDB操作框架Autofac对接框架 - 默认的注册实现
// 创建标识：胡迪 2018.07.04
//----------------------------------------------------------------*/
using Autofac;
using AutofacMiddleware;
using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBAutofacMiddlewareImp
{
    /// <summary>
    /// 默认的注册实现
    /// </summary>
    internal class DefaultAutofacContainerPrepare : IAutofacContainerPrepare
    {
        private Type m_useContextType = null;

        private static Type m_useBaseType = typeof(BaseMongoDBContext);

        private  bool m_ifAsinputType = false;

        internal DefaultAutofacContainerPrepare(Type inputType,bool ifAsInputType)
        {
            m_useContextType = inputType;
            m_ifAsinputType = ifAsInputType;
        }

        public void Prepare(ContainerBuilder builder)
        {
            if (m_ifAsinputType)
            {
                builder.RegisterType(m_useContextType).AsSelf().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType(m_useContextType).As(m_useBaseType).InstancePerLifetimeScope();
            }
        }
    }
}
