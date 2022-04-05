using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProjectCore.Business.Abstract;
using ExamProjectCore.Business.Concrete;
using ExamProjectCore.DataAccess.Abstract;
using ExamProjectCore.DataAccess.Concrete;
using ExamProjectCore.WebUI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExamProjectCore.WebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IExamDal, EFCoreExamDal>();
            services.AddScoped<IExamService, ExamManager>();

            services.AddScoped<IQuestionDal, EFCoreQuestionDal>();
            services.AddScoped<IQuestionService, QuestionManager>();

            services.AddMvc()
              .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //SeedDatabase.Seed();
            }

            app.UseStaticFiles();
            app.CustomStaticFiles();
            //app.UseMvcWithDefaultRoute();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
