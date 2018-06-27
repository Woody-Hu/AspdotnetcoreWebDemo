using AutoEFContextRepository;
using AutofacAopImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.DAO;
using WebDemo.Entity;
using WebDemo.Utility;

namespace WebDemo.Service
{
    public interface ITestService
    {

        [EFCoreTransaction(typeof(SqliteContext))]
        [Log]
        bool Add(string inputText);

        [Log]
        List<TestEntity> Get();

        [Log]
        PagePacker<TestEntity> GetPage(int nowPage, int inputPageSize);

    }
}
