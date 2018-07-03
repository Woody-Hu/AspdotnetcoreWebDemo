using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Entity;
using WebDemo.Service;

namespace WebDemo.Controllers
{

    [Produces("application/json")]
    [Route("api/TestMongoDB")]
    public class TestMongoDBController : Controller
    {
        readonly ITestMongoDBService m_useService;

        public TestMongoDBController(ITestMongoDBService inputTestService)
        {
            m_useService = inputTestService;
        }

        [HttpGet("add")]
        public bool Add()
        {
            return m_useService.Add(new TestMongoDBEntity() { Name = "aa",Value = 5 });
        }

        [HttpGet("get")]
        public List<TestMongoDBEntity> Get()
        {
            return m_useService.GetAll();
        }




    }
}
