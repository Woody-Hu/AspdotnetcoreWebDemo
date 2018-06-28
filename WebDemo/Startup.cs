using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoEFContextRepository;
using Autofac;
using AutofacEFImp;
using AutofacMiddleware;
using AutofacMiddlewarePrepare;
using AutofacUtility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebDemo.DAO;
using WebDemo.Entity;
using NLog.Extensions.Logging;
using NLog.Web;

namespace WebDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //将当前的Mvc系统控制器注册为服务
            services.AddMvc().AddControllersAsServices() ;
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {

            //加载依赖程序集
            var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var oneName in assemblies)
            {
                Assembly.Load(oneName);
            }

            //注册扩展服务
            List<IAutofacContainerPrepare> tempList = new List<IAutofacContainerPrepare>();
            //HttpContext获取器
            tempList.Add(new AutofacHttpContextAccessorPrepare());
            //数据库上下文
            tempList.AddRange(AutoGenericAutoEFAutofacContainerPrepare.GetPrepares());
            builder.RegisterModule(new AutofacModule(tempList));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
    
            //指定配置文件
            env.ConfigureNLog("nlog.config");


            //若是开发模式 打开详细异常中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //静态文件处理中间件
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //添加Nlog
            loggerFactory.AddNLog();


            //使用mvc管道中间件
            app.UseMvc();
        }
    }
}
