﻿
using System.Collections.Generic;
using System.Reflection;
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
using NLog.Extensions.Logging;
using NLog.Web;
using MongoDBAutofacMiddlewareImp;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System;

namespace WebDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //若是开发模式 注册swagger
            PrepareSwaggerService(services);

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

            //MongoDB数据库上下文
            tempList.AddRange(AutoMogoDBPreparer.GetPrepares());

            builder.RegisterModule(new AutofacModule(tempList));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
    
            //指定配置文件
            env.ConfigureNLog("nlog.config");

            //添加Nlog
            loggerFactory.AddNLog();


            //若是开发模式 打开详细异常中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //静态文件处理中间件
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //若是开发模式 打开Swagger中间件
            if (env.IsDevelopment())
            {
                //Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Doc");
                });
            }

            //使用mvc管道中间件
            app.UseMvc();
        }

        /// <summary>
        /// 配置Swagger服务
        /// </summary>
        /// <param name="services"></param>
        private void PrepareSwaggerService(IServiceCollection services)
        {
            //若是开发模式 注册swagger
            if (Environment.IsDevelopment())
            {
                //使用的注释文档路径
                string useXmlPath = String.Format(@"{0}\{1}.xml",
                            System.AppDomain.CurrentDomain.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name);
                //注册Swagger服务
                services.AddSwaggerGen(c =>
                {
                    {
                        c.SwaggerDoc("v1", new Info
                        {
                            Version = "v1",
                            Title = "Web API",
                            Description = "Web API Doc",
                            TermsOfService = "None",
                        });

                        if (File.Exists(useXmlPath))
                        {
                            //设置Control注释文档
                            c.IncludeXmlComments(useXmlPath, true);
                        }
                    }
                });
            }
        }
    }
}
