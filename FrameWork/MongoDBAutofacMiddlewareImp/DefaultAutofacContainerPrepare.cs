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
