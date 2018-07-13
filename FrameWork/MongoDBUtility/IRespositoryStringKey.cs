/*----------------------------------------------------------------
// Copyright (C) 2015 新鸿业科技有限公司
// 版权所有。 
// MongoDB操作框架 - String键型操作接口
// 创建标识：胡迪 2018.07.04
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBUtility
{
    /// <summary>
    /// String键型操作接口
    /// </summary>
    /// <typeparam name="C"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IRespositoryStringKey <C,T>: IRepository<C, T,string>
        where T : IEntity<string>
        where C : BaseMongoDBContext
    {
    }
}
