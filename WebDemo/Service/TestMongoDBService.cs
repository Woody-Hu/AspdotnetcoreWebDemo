using AutofacMiddleware;
using MongoDBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.DAO;
using WebDemo.Entity;

namespace WebDemo.Service
{
    [ComponentAttribute(IfByClass = false, LifeScope = LifeScopeKind.Request)]
    public class TestMongoDBService : ITestMongoDBService
    {
        private readonly IRespositoryStringKey<TestMongoDBContext, TestMongoDBEntity> m_useRepository;

        public TestMongoDBService(IRespositoryStringKey<TestMongoDBContext, TestMongoDBEntity> inputValue)
        {
            m_useRepository = inputValue;
        }


        public bool Add(TestMongoDBEntity inputEntity)
        {
            m_useRepository.Add(inputEntity);
            return true;
        }

        public List<TestMongoDBEntity> GetAll()
        {
            return m_useRepository.Get(null).ToList();
        }
    }
}
