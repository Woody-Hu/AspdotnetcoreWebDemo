using AutoEFContext;
using AutoEFContextRepository;
using AutofacMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.DAO;
using WebDemo.Entity;

namespace WebDemo.Service
{
    [ComponentAttribute(IfByClass = false,LifeScope = LifeScopeKind.Request)]
    public class TestService: ITestService
    {
        readonly IRepository<SqliteContext, TestEntity> m_useRepository;

        public TestService(IRepository<SqliteContext, TestEntity> inputRepository)
        {
            m_useRepository = inputRepository;
        }

        public bool Add(string inputText)
        {
            TestEntity tempEntity = new TestEntity();
            tempEntity.Value = inputText;

            m_useRepository.Add(tempEntity);
            return true;
        }

        public List<TestEntity> Get()
        {
            return m_useRepository.GetAll();
        }

        public PagePacker<TestEntity> GetPage(int nowPage, int inputPageSize)
        {
            return m_useRepository.GetPage(nowPage, inputPageSize);
        }
    }
}
