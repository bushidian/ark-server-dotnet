using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ArkApplication.Framework.Caching;
using ArkApplication.Framework.NoSql;
using ArkApplication.Framework.Data;
using System;

namespace ArkApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("config.json",optional: true, reloadOnChange:true)
                .AddEnvironmentVariables();
                
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                // builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            services.AddMvc();
            services.AddCors();
            // Add application services.

            services.AddSingleton(provider => Configuration);

            var cacheType = CacheTypes.None;
            Enum.TryParse(Configuration["Cache:Type"], out cacheType);
            InitCacheProvider(services, cacheType);

            var nosqlType = NoSqlTypes.None;
            Enum.TryParse(Configuration["NoSql:Type"], out nosqlType);
            InitNoSqlProvider(services, nosqlType);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
           
            app.UseCors(builder=> builder.AllowAnyOrigin());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitDbConnection(IServiceCollection services, DbTypes type)
        {
              switch(type){
                  case DbTypes.SqlServer:
                      services.AddDbContext<ArkDbContext>(options =>
                          options.UseSqlServer(Configuration["Data:SqlServerConnection"]));
                  break;
                  case DbTypes.MySql:
                     services.AddDbContext<ArkDbContext>(options =>
                         options.UseMySql(Configuration["Data:MySqlConnection"]));
                  break;
                  case DbTypes.PostgreSql:
                     services.AddDbContext<ArkDbContext>(options =>
                         options.UseNpgsql(Configuration["Data:PostgreSqlConnection"]));
                  break;
                  case DbTypes.SqlLite:
                      services.AddDbContext<ArkDbContext>(options =>
                         options.UseSqlite(Configuration["Data:SqlLiteConnection"]));
                  break;
              }
        }

        private void InitCacheProvider(IServiceCollection services, CacheTypes type)
        {
            switch(type)
            {
                case CacheTypes.Memory:
                   services.AddSingleton<ICacheManager, MemoryCacheManager>();
                   break;
                case CacheTypes.Redis:
                   services.AddSingleton<ICacheManager, RedisCacheManager>();
                   break;
                case CacheTypes.None:
                   services.AddSingleton<ICacheManager, NullCacheManager>();
                   break;
            }
        }

        private void InitNoSqlProvider(IServiceCollection services, NoSqlTypes type)
        {
            switch(type)
            {
                case NoSqlTypes.Mongo:
                   services.AddScoped(typeof(INoSqlRepository<>), typeof(MongoRepository<>));
                   break;
            }
        }

    }
}
