using AutofacMiddleware;
using MongoDB.Driver;
using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDBAutofacMiddlewareImp
{
    [Component(IfByClass = false, LifeScope = LifeScopeKind.Transient)]
    /// <summary>
    /// 默认持久化操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultRepository<C, T> : IRepository<C, T, string>
       where T : DefaultEntity
       where C : BaseMongoDBContext
    {
        private readonly IMongoCollection<T> m_useDBSet;

        private readonly IMongoDatabase m_useDataBase;

        private readonly C m_useContext;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="inputContext"></param>
        public DefaultRepository(C inputContext)
        {
            m_useContext = inputContext;
            m_useDataBase = m_useContext.UseDataBase;
            m_useDBSet = m_useDataBase.GetCollection<T>(MongoDBEntityAttribute.GetUseName(typeof(T)));
        }

        /// <summary>
        /// 添加一个
        /// </summary>
        /// <param name="inputEntity"></param>
        public void Add(T inputEntity)
        {
            AddMethod(inputEntity).Wait();
        }

        /// <summary>
        /// 添加一组
        /// </summary>
        /// <param name="inputEntitys"></param>
        public void AddRange(IEnumerable<T> inputEntitys)
        {
            AddRangeMethod(inputEntitys).Wait();
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="input"></param>
        public void Delete(T input)
        {
            DeleteMethod(input).Wait();
        }

        public T Get(string id)
        {
            return GetMethod(k => k.Id == id).Result.First();
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <param name="useWhere"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> useWhere = null)
        {
            return GetMethod(useWhere).Result.ToEnumerable();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        public void Update(T input)
        {
            UpdateMethod(input).Wait();
        }

        /// <summary>
        /// 内部获取方法NIO
        /// </summary>
        /// <param name="useWhere"></param>
        /// <returns></returns>
        private async Task<IAsyncCursor<T>> GetMethod(Expression<Func<T, bool>> useWhere = null)
        {
            if (null == useWhere)
            {
                return await m_useDBSet.FindAsync(k => true);
            }
            else
            {
                return await m_useDBSet.FindAsync(useWhere);
            }
        }

        /// <summary>
        /// 更新方法NIO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task UpdateMethod(T input)
        {
            await m_useDBSet.FindOneAndReplaceAsync(k => k.Id == input.Id, input);
        }

        /// <summary>
        /// 添加方法NIO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddMethod(T input)
        {
            await m_useDBSet.InsertOneAsync(input);
        }

        /// <summary>
        /// 成组添加NIO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task AddRangeMethod(IEnumerable<T> input)
        {
            await m_useDBSet.InsertManyAsync(input);
        }

        /// <summary>
        /// 删除方法NIO
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task DeleteMethod(T input)
        {
            await m_useDBSet.FindOneAndDeleteAsync(k => k.Id == input.Id);
        }
    }
}
