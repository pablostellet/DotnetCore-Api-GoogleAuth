using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using base_template.Data;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http;
using base_template.Entities;
using Microsoft.AspNetCore.Builder;
using DotNetCore.AspNetCore;
using DotNetCore.IoC;

namespace base_template
{
    public static class StartupExtensions
    {
        public static bool IsDev(this IConfiguration configuration)
        {
            var isProd = Convert.ToBoolean(configuration["IsProduction"]);
            return !isProd;
        }

        // Para testes em base de dados local
        public static void AddContextSqlite(this IServiceCollection services)
        {
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlite("Data source=users.sqlite",
                sqliteOptions => sqliteOptions.MigrationsAssembly("GoogleSpaWeb"))
            );
            services.AddIdentity();
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddContextSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Configure permissions for Cors
        /// </summary>
        public static void AddCorsPolicy(this IServiceCollection services, string corsPolicy)
        {
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(corsPolicy,
                    configurePolicy => configurePolicy
                        .WithOrigins("http://localhost:4200", "https://localhost:4200", "null")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });
        }

        public static void AddGoogleAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication()
                .AddOpenIdConnect(
                    authenticationScheme: "Google",
                    displayName: "Google",
                    options =>
                    {
                        options.Authority = "https://accounts.google.com/";
                        options.CallbackPath = new PathString("/google-callback");
                        options.ClientId = Configuration["Google:ClientId"];
                        options.ClientSecret = Configuration["Google:ClientSecret"];
                        options.Scope.Add("email");
                        options.Scope.Add("profile");
                    }
                );
        }

        public static void AddSecurity(this IServiceCollection services)
        {
            services.AddHash(10000, 128);
            services.AddJsonWebToken(Guid.NewGuid().ToString(), TimeSpan.FromHours(12));
            services.AddAuthenticationJwtBearer();
        }

    }
}