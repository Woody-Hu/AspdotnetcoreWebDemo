
using AutoEFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 表达式树工具
    /// </summary>
    public static class ExpressionUtility
    {
        #region 私有字段
        /// <summary>
        /// 使用的线程安全类型Include表达式树字典
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> m_useIncludeDic = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 使用的线程安全类型Load表达式树字典
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> m_useLoadDic = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 使用的自动多层Include表达式树字典
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> m_useLinkIncludeDic = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 使用的自动上下文特性
        /// </summary>
        private static readonly Type m_useAutoEntityType = typeof(AutoEntityAttribute);

        /// <summary>
        /// 使用的迭代类型
        /// </summary>
        private static readonly Type m_useIEnumableType = typeof(IEnumerable<>); 
        #endregion

        /// <summary>
        /// 多层Include机制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<IQueryable<T>, IQueryable<T>> GetLinkInclude<T>()
             where T : class
        {
            Func<IQueryable<T>, IQueryable<T>> returnValue = null;

            var tempType = typeof(T);

            if (!m_useLinkIncludeDic.ContainsKey(tempType))
            {
                m_useLinkIncludeDic.GetOrAdd(tempType, GetLinkIncludeExpression<T>());
            }

            returnValue = m_useLinkIncludeDic[tempType] as Func<IQueryable<T>, IQueryable<T>>;

            return returnValue;
        }

        /// <summary>
        /// 获取Load机制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<T,DbContext,T> GetLoad<T>()
            where T:class
        {
            Func<T, DbContext, T> returnValue = null;

            var tempType = typeof(T);

            if (!m_useLoadDic.ContainsKey(tempType))
            {
                m_useLoadDic.GetOrAdd(tempType, GetLoadExpression<T>());
            }

            returnValue = m_useLoadDic[tempType] as Func<T, DbContext, T>;

            return returnValue;
        }

        /// <summary>
        /// 获取Include机制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Func<IQueryable<T>, IQueryable<T>> GetInclude<T>()
            where T:class
        {
            Func<IQueryable<T>, IQueryable<T>> returnValue = null;

            var tempType = typeof(T);

            if (!m_useIncludeDic.ContainsKey(tempType))
            {
                m_useIncludeDic.GetOrAdd(tempType, GetIncludeExpression<T>());
            }

            returnValue = m_useIncludeDic[tempType] as Func<IQueryable<T>, IQueryable<T>>;

            return returnValue;
        }

        /// <summary>
        /// 利用输入类的属性与输入容器制作表达式树
        /// </summary>
        /// <typeparam name="TClass">使用的类泛型</typeparam>
        /// <typeparam name="TProperty">使用的类属性泛型</typeparam>
        /// <param name="inputProperty">使用的获取属性的Func</param>
        /// <param name="inputContainsSet">使用的容器</param>
        /// <param name="inputComparer">使用的比较Func 第一个参数为从输入中取出的值，第二个参数为从容器中取出的值</param>
        /// <returns>使用的表达式树</returns>
        public static Expression<Func<TClass, bool>> GetEqulaOrExpression<TClass, TProperty>
            (Func<TClass, TProperty> inputProperty, IEnumerable<TProperty> inputContainsSet, Func<TProperty, TProperty, bool> inputComparer = null)
        {
            if (null == inputComparer)
            {
                inputComparer = GetDefaultFunc<TProperty>();
            }

            //获取输入的参数
            ParameterExpression useInputExpression = Expression.Parameter(typeof(TClass));

            var tempMethod = inputProperty.Method;

            var tempComparerMethod = inputComparer.Method;

            var useValue = Expression.Call(tempMethod, useInputExpression);

            List<ConstantExpression> lstUseConstrant = new List<ConstantExpression>();

            foreach (var oneValue in inputContainsSet)
            {
                lstUseConstrant.Add(Expression.Constant(oneValue, typeof(TProperty)));
            }

            Expression returnExpression = Expression.Constant(false, typeof(bool));

            //表达组合
            foreach (var oneExpression in lstUseConstrant)
            {
                returnExpression = Expression.Or(returnExpression, Expression.Call(tempComparerMethod, useValue, oneExpression));
            }


            return Expression.Lambda<Func<TClass, bool>>(returnExpression, useInputExpression);
        }

        #region 私有方法
        /// <summary>
        /// 使用的默认比较器
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        private static Func<TProperty, TProperty, bool> GetDefaultFunc<TProperty>()
        {
            return (k1, k2) => k1.Equals(k2);
        }

        /// <summary>
        /// 获取Load表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Func<T, DbContext, T> GetLoadExpression<T>()
            where T : class
        {
            var tempType = typeof(T);

            List<string> useCollectionString = new List<string>();

            List<string> useReferenceString = new List<string>();

            foreach (var oneProperty in tempType.GetProperties())
            {
                if (oneProperty.PropertyType.IsGenericType && (
                    m_useIEnumableType.MakeGenericType(oneProperty.PropertyType.GetGenericArguments()[0])).IsAssignableFrom(oneProperty.PropertyType))
                {
                    useCollectionString.Add(oneProperty.Name);
                }
                else if (null != oneProperty.PropertyType.GetCustomAttribute(m_useAutoEntityType))
                {
                    useReferenceString.Add(oneProperty.Name);
                }
            }

            if (0 == useCollectionString.Count && 0 == useReferenceString.Count)
            {
                return null;
            }
            else
            {
                return (k, d) =>
                {

                    foreach (var oneString in useCollectionString)
                    {
                        d.Entry(k).Collection(oneString).LoadAsync().Wait();

                    }

                    foreach (var oneReferenceString in useReferenceString)
                    {
                        d.Entry(k).Reference(oneReferenceString).LoadAsync().Wait();
                    }

                    return k;
                };
            }
        }

        /// <summary>
        /// 自动Include机制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Func<IQueryable<T>, IQueryable<T>> GetIncludeExpression<T>()
            where T : class
        {
            var tempType = typeof(T);

            List<string> usePropertyString = new List<string>();


            foreach (var oneProperty in tempType.GetProperties())
            {
                if (IfPropertyNeedInclude(oneProperty))
                {
                    usePropertyString.Add(oneProperty.Name);
                }
            }

            if (0 != usePropertyString.Count)
            {
                return k =>
                {
                    foreach (var oneString in usePropertyString)
                    {
                        k.Include(oneString);
                    }

                    return k;
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断属性是否需要Include
        /// </summary>
        /// <param name="oneProperty"></param>
        /// <returns></returns>
        private static bool IfPropertyNeedInclude(PropertyInfo oneProperty)
        {
            return IfTypeIsEnumerableType(oneProperty)
                                || null != oneProperty.PropertyType.GetCustomAttribute(m_useAutoEntityType);
        }

        /// <summary>
        /// 判断属性是否是可迭代类型的
        /// </summary>
        /// <param name="oneProperty"></param>
        /// <returns></returns>
        private static bool IfTypeIsEnumerableType(PropertyInfo oneProperty)
        {
            return (oneProperty.PropertyType.IsGenericType && (
                                            m_useIEnumableType.MakeGenericType(oneProperty.PropertyType.GetGenericArguments()[0])).IsAssignableFrom(oneProperty.PropertyType));
        }

        /// <summary>
        /// 获得多层Include表达式树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static Func<IQueryable<T>, IQueryable<T>> GetLinkIncludeExpression<T>()
             where T : class
        {
            HashSet<string> useStrings = new HashSet<string>();
            HashSet<PropertyInfo> visitedProperty = new HashSet<PropertyInfo>();

            //获取全部Include表达
            GetLinkIncludeExpression(ref useStrings, ref visitedProperty, string.Empty, typeof(T));

            if (0 == useStrings.Count)
            {
                return null;
            }
            else
            {
                return k =>
                {
                    foreach (var oneString in useStrings)
                    {
                        k = k.Include(oneString);
                    }

                    return k;
                };
            }
        }

        /// <summary>
        /// 获得向下递归Include表达式字符串
        /// </summary>
        /// <param name="useStrings"></param>
        /// <param name="visitedProperty"></param>
        /// <param name="inputString"></param>
        /// <param name="inputType"></param>
        private static void GetLinkIncludeExpression
            (ref HashSet<string> useStrings, ref HashSet<PropertyInfo> visitedProperty, string inputString, Type inputType)
        {
            List<PropertyInfo> lstUsedPropertyInfo = new List<PropertyInfo>();

            //获取需要向下访问的属性
            foreach (var oneProperty in inputType.GetProperties())
            {
                if (IfPropertyNeedInclude(oneProperty) && !visitedProperty.Contains(oneProperty))
                {
                    lstUsedPropertyInfo.Add(oneProperty);
                }
            }

            if (0 == lstUsedPropertyInfo.Count)
            {
                if (!string.IsNullOrWhiteSpace(inputString))
                {
                    useStrings.Add(inputString);
                }
            }
            else
            {
                foreach (var oneProperty in lstUsedPropertyInfo)
                {
                    //生成向下字符串
                    string tempString = string.IsNullOrWhiteSpace(inputString) ? oneProperty.Name : inputString + "." + oneProperty.Name;

                    //添加到访问列表
                    visitedProperty.Add(oneProperty);

                    Type useNextType = oneProperty.PropertyType;

                    if (IfTypeIsEnumerableType(oneProperty))
                    {
                        useNextType = oneProperty.PropertyType.GetGenericArguments()[0];
                    }

                    //向下递归
                    GetLinkIncludeExpression(ref useStrings, ref visitedProperty, tempString, useNextType);
                }
            }


        } 
        #endregion
    }
}
