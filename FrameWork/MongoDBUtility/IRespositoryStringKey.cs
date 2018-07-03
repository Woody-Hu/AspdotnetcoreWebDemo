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
