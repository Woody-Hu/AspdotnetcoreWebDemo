using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoEFContextRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDemo.Entity;
using WebDemo.Service;
using WebDemo.Utility;

namespace WebDemo.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [LogActionFilter]
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        readonly ITestService m_useService;

        public TestController(ITestService inputTestService)
        {
            m_useService = inputTestService;
        }


        /// <summary>
        /// 测试添加方法
        /// </summary>
        /// <returns></returns>
        [HttpGet("add")]
        public bool Add()
        {
            return m_useService.Add("aaaa");
        }

        [HttpGet("get")]
        public List<TestEntity> Get()
        {
            return m_useService.Get();
        }

        [HttpGet("getpage")]
        public PagePacker<TestEntity> GetPage()
        {
            return m_useService.GetPage(2, 2);
        }

       
    }
}