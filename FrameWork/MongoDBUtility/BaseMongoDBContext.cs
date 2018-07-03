using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBUtility
{
    /// <summary>
    /// 连接上下文底层
    /// </summary>
    public abstract class BaseMongoDBContext
    {
        private MongoClient m_useClient = null;

        /// <summary>
        /// 使用的Client
        /// </summary>
        public MongoClient UseClient { get => m_useClient;private set => m_useClient = value; }

        /// <summary>
        /// 使用的DataBase
        /// </summary>
        public IMongoDatabase UseDataBase { get; private set; }

        /// <summary>
        /// 使用的连接字符串
        /// </summary>
        private string m_thisConectionString = null;

        /// <summary>
        /// 使用的Set名称
        /// </summary>
        private string m_thisDataSetName = null;

        public BaseMongoDBContext()
        {
            //设置连接字符串
            Prepare(ref m_thisConectionString);

            //创建MongoClient
            UseClient = new MongoClient(m_thisConectionString);

            //获取库名
            m_thisDataSetName = AutoMongoDBContextAttribue.GetUseDataSetName(this.GetType());

            //获取数据库
            UseDataBase = UseClient.GetDatabase(m_thisDataSetName);
        }

        /// <summary>
        /// 抽象方法
        /// </summary>
        /// <param name="useContectionString"></param>
        protected abstract void Prepare(ref string useContectionString);


    }
}
