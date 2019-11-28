using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.AspNetCore;
using DotNetCore.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace base_template
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureLogger();
        }

        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(Configuration)
                            .CreateLogger();

            Log.Information("Starting up");
        }

        public IConfiguration Configuration { get; }
        private const string corsPolicy = "CorsPolicy";


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();
            //loggerFactory.AddSerilog();

            app.UseCors(corsPolicy);
            //app.UseCorsAllowAny();

            app.UseHttpsRedirection();
            //app.UseRouting();
            //app.UseHttps();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseMvcWithDefaultRoute();
            //app.UseEndpoints(endpoints => endpoints.MapControllers());

            //app.UseSerilogRequestLogging();

            //app.UseCors("apps");
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLog();
            //services.AddCors();
            services.AddCorsPolicy(corsPolicy);
            services.AddSecurity();
            services.AddGoogleAuthentication(Configuration);
            services.AddResponseCompression();
            services.AddResponseCaching();
            services.AddControllers();
            services.AddMvc(MvcOptions => MvcOptions.EnableEndpointRouting = false);
            services.AddFileService();
            services.AddContextSqlite();
            services.AddClassesMatchingInterfaces();
            //services.AddContextSqlServer("connectionString");
            //services.AddControllers();
            //services.AddMvc(option => option.EnableEndpointRouting = false);
        }

    }
}
