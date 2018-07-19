
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebDemo.Utility
{
    /// <summary>
    /// 设置封装单例模式
    /// </summary>
    public class ConfigPacker
    {
        #region 私有字段
        /// <summary>
        /// 使用的内部读写锁
        /// </summary>
        private ReaderWriterLockSlim m_useLocker = new ReaderWriterLockSlim();

        /// <summary>
        /// 内部读写分离字典
        /// </summary>
        private _RWDic m_innerDic = new _RWDic();

        /// <summary>
        /// 使用的设置封装
        /// </summary>
        private IConfiguration m_useConfig = null;

        /// <summary>
        /// 私有构造方法
        /// </summary>
        private ConfigPacker()
        {
            //获取appsetting配置文件
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var configRoot = builder.Build();

            this.UseConfig = configRoot;

        }
        #endregion

        /// <summary>
        /// 设置封装 读写分离
        /// </summary>
        public IConfiguration UseConfig
        {
            private set
            {
                try
                {
                    m_useLocker.EnterWriteLock();
                    m_useConfig = value;
                }
                finally
                {
                    m_useLocker.ExitWriteLock();
                }
            }

            get
            {
                try
                {
                    m_useLocker.EnterReadLock();
                    return m_useConfig;
                }
                finally
                {
                    m_useLocker.ExitReadLock();
                }
            }
        }

        public T GetConfig<T>(string inputSectionName = null)
            where T : class, new()
        {
            //调整SectionName
            if (string.IsNullOrWhiteSpace(inputSectionName))
            {
                inputSectionName = typeof(T).Name;
            }

            T returnValue = null;

            //判断是否有缓存
            if (m_innerDic.Contains(inputSectionName))
            {
                return m_innerDic.Get(inputSectionName) as T;
            }
            else
            {
                returnValue = MakeOneValue<T>(inputSectionName);
                m_innerDic.Add(inputSectionName, returnValue);
            }



            return returnValue;
        }

        /// <summary>
        /// 单例模式获取
        /// </summary>
        /// <returns></returns>
        public static ConfigPacker GetConfigPacker()
        {
            return _ConfigPacker.UseSingleton;
        }

        #region 私有方法
        /// <summary>
        /// 制作一个变量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputSectionName"></param>
        /// <returns></returns>
        private T MakeOneValue<T>(string inputSectionName) where T : class, new()
        {
            var tempValue = UseConfig.GetSection(inputSectionName);

            T returnValue = tempValue.Get<T>();

            return returnValue;
        }

        /// <summary>
        /// 内部类实现线程安全单例模式
        /// </summary>
        private class _ConfigPacker
        {
            internal static ConfigPacker UseSingleton = new ConfigPacker();
        }

        /// <summary>
        /// 内部读写分离模式字典
        /// </summary>
        private class _RWDic
        {
            /// <summary>
            /// 使用的内部读写锁
            /// </summary>
            private ReaderWriterLockSlim m_useLocker = new ReaderWriterLockSlim();

            /// <summary>
            /// 内部字典
            /// </summary>
            private Dictionary<string, object> m_useDic = new Dictionary<string, object>();

            /// <summary>
            /// 添加
            /// </summary>
            /// <param name="inputKey"></param>
            /// <param name="inputValue"></param>
            internal void Add(string inputKey, object inputValue)
            {
                try
                {
                    m_useLocker.EnterWriteLock();

                    if (m_useDic.ContainsKey(inputKey))
                    {
                        m_useDic[inputKey] = inputValue;
                    }
                    else
                    {
                        m_useDic.Add(inputKey, inputValue);
                    }
                }
                finally
                {
                    m_useLocker.ExitWriteLock();
                }
            }

            /// <summary>
            /// 是否包含
            /// </summary>
            /// <param name="inputKey"></param>
            /// <returns></returns>
            internal bool Contains(string inputKey)
            {
                try
                {
                    m_useLocker.EnterReadLock();

                    return m_useDic.ContainsKey(inputKey);
                }
                finally
                {
                    m_useLocker.ExitReadLock();
                }
            }

            /// <summary>
            /// 获取
            /// </summary>
            /// <param name="inputKey"></param>
            /// <returns></returns>
            internal object Get(string inputKey)
            {
                try
                {
                    m_useLocker.EnterReadLock();

                    return m_useDic[inputKey];
                }
                finally
                {
                    m_useLocker.ExitReadLock();
                }
            }
        }
        #endregion


    }
}
