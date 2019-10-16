using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication
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
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use((context, next) =>
            {
                context.Request.EnableBuffering();
                return next();
            });
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Request.EnableBuffering();

                    var reader = await context.Request.BodyReader.ReadAsync();
                    var buffer = reader.Buffer;
                    var sp = Encoding.UTF8.GetString(buffer.FirstSpan);
                    Console.WriteLine("From handler");
                    Console.WriteLine(sp);
                    Console.WriteLine("From handler end");
                });
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}