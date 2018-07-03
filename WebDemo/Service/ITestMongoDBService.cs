using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Entity;

namespace WebDemo.Service
{
    public interface ITestMongoDBService
    {
        bool Add(TestMongoDBEntity inputEntity);

        List<TestMongoDBEntity> GetAll();
    }
}
