
using AutoEFContext;
using AutoEFContextRepository;
using AutofacMiddleware;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebDemo.DAO
{
    /// <summary>
    /// 扩展数据操作接口实现
    /// </summary>
    /// <typeparam name="T">使用的上下文类型</typeparam>
    /// <typeparam name="X">使用的Entity类型</typeparam>
    [Component(IfByClass = false, LifeScope = LifeScopeKind.Transient)]
    public class DefaultRepositorExtension<T, X> : IRepositorExtension<T, X>
        where X : class
        where T : AutoContext
    {
        #region 私有字段
        /// <summary>
        /// 使用的存储层接口
        /// </summary>
        private readonly IRespository<T, X> m_coreRepository = null;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputRepository"></param>
        public DefaultRepositorExtension(IRespository<T, X> inputRepository)
        {
            m_coreRepository = inputRepository;
        }

        /// <summary>
        /// 使用的DbSet
        /// </summary>
        public DbSet<X> UseDB => m_coreRepository.UseDB;

        /// <summary>
        /// 使用的Context
        /// </summary>
        public T UseContext => m_coreRepository.UseContext;

        /// <summary>
        /// 添加一个
        /// </summary>
        /// <param name="input"></param>
        public void Add(X input)
        {
            m_coreRepository.Add(input);
        }

        /// <summary>
        /// 添加一组
        /// </summary>
        /// <param name="input"></param>
        public void AddRange(IEnumerable<X> input)
        {
            m_coreRepository.AddRange(input);
        }


        /// <summary>
        /// 删除一个
        /// </summary>
        /// <param name="input"></param>
        public void Delete(X input)
        {
            m_coreRepository.Delete(input);
        }

        /// <summary>
        /// 寻找符合条件的第一个
        /// </summary>
        /// <param name="useWhere">使用的过滤条件</param>
        /// <returns></returns>
        public X FindFirst(Expression<Func<X, bool>> useWhere = null, IncludeDel<X> useInclude = null)
        {
            return m_coreRepository.FindFirst(useWhere, useInclude);
        }

        /// <summary>
        /// 寻找所有
        /// </summary>
        /// <param name="useWhere">使用的过滤条件</param>
        /// <returns></returns>
        public List<X> GetAll(Expression<Func<X, bool>> useWhere = null, IncludeDel<X> useInclude = null)
        {
            return m_coreRepository.GetAll(useWhere, useInclude);

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="usePage">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="useWhere">使用的过滤条件</param>
        /// <returns></returns>
        public PagePacker<X> GetPage(int usePage, int pageSize, Expression<Func<X, bool>> useWhere = null, IncludeDel<X> useInclude = null)
        {
            return m_coreRepository.GetPage(usePage, pageSize, useWhere, useInclude);

        }


        /// <summary>
        /// 附带转换机制的获取全部
        /// </summary>
        /// <typeparam name="Y">转换后的类型</typeparam>
        /// <param name="useTransformer">使用的转换机制（如group操作）</param>
        /// <param name="useWhere">使用的过滤条件</param>
        /// <param name="useInclude">使用的Include委托</param>
        /// <returns></returns>
        public List<Y> GetAll<Y>(Func<IQueryable<X>, IQueryable<Y>> useTransformer, Expression<Func<Y, bool>> useWhere = null, IncludeDel<Y> useInclude = null)
            where Y : class
        {
            return m_coreRepository.GetAll(useTransformer, useWhere, useInclude);
        }

        /// <summary>
        /// 附带转换机制的获取第一个
        /// </summary>
        /// <typeparam name="Y">转换后的类型</typeparam>
        /// <param name="useTransformer">使用的转换机制（如group操作）</param>
        /// <param name="useWhere">使用的过滤条件</param>
        /// <param name="useInclude">使用的Include委托</param>
        /// <returns></returns>
        public Y FindFirst<Y>(Func<IQueryable<X>, IQueryable<Y>> useTransformer, Expression<Func<Y, bool>> useWhere = null, IncludeDel<Y> useInclude = null)
            where Y : class
        {
            return m_coreRepository.FindFirst(useTransformer, useWhere, useInclude);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="usePage">查询的页数</param>
        /// <param name="pageSize">每页的容量</param>
        /// <param name="useWhere">使用的过滤条件</param>
        /// <param name="useInclude">使用的Include委托</param>
        /// <returns></returns>
        public PagePacker<Y> GetPage<Y>(Func<IQueryable<X>, IQueryable<Y>> useTransformer, int usePage, int pageSize, Expression<Func<Y, bool>> useWhere = null, IncludeDel<Y> useInclude = null)
            where Y : class
        {
            return m_coreRepository.GetPage(useTransformer, usePage, pageSize, useWhere, useInclude);
        }


        /// <summary>
        /// 更新一个
        /// </summary>
        /// <param name="input"></param>
        public void Update(X input)
        {
            m_coreRepository.Update(input);
        }


       
    }
}
