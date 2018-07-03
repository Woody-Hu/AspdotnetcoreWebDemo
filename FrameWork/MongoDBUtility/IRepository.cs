using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoDBUtility
{
    public interface IRepository<C,T,in TKey>
        where T : IEntity<TKey>
        where C: BaseMongoDBContext
    {
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(TKey id);

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> useWhere);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="inputEntity"></param>
        void Add(T inputEntity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="inputEntitys"></param>
        void AddRange(IEnumerable<T> inputEntitys);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        void Update(T input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        void Delete(T input);
    }
}
