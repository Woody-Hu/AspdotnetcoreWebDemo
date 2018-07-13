/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// Autofac扫描注入框架 - AOP机制 - 使用的代理判断钩子
// 创建标识：胡迪 2018.07.03
//----------------------------------------------------------------*/
using AutofacMiddleware;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutofacUtility
{
    /// <summary>
    /// 使用的代理判断钩子
    /// </summary>
    internal class DefaultProxyGenerationHook : IProxyGenerationHook
    {
        /// <summary>
        /// 使用的创造器类型
        /// </summary>
        private static Type m_useCreaterType = typeof(AbstractInterceptorAttribute);

        public void MethodsInspected()
        {
            ;
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            ;
        }

        /// <summary>
        /// 判断方法是否需要AOP
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return 0 != methodInfo.GetCustomAttributes(m_useCreaterType, false).Length;
        }
    }
}
