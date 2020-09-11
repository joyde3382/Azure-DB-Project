using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E18I4DABH4Gr4.Context;
using E18I4DABH4Gr4.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using E18I4DABH4Gr4.Models;
using E18I4DABH4Gr4.Repositories;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
// Git ignore

namespace E18I4DABH4Gr4
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            //services.AddTransient(typeof(SmartGridRepository));

            // Create Database TraderDb
            //string mDbName = "TraderDB";
            //string mConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=" + mDbName + ";Integrated Security=True;" +
            //    "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string mConnString = "Data Source=st-i4dab.uni.au.dk;User ID=E18I4DABH4Gr4;Password=E18I4DABH4Gr4;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<TraderContext>(options => options.UseSqlServer(mConnString));

            // Create Database SmartGridDB
            //string mDbName2 = "SmartGridDB";
            //string mConnString2 = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=" + mDbName2 + ";Integrated Security=True;" +
            //    "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string mConnString2 = "Data Source=st-i4dab.uni.au.dk;User ID=E18I4DABH4Gr4;Password=E18I4DABH4Gr4;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<SmartGridDBContext>(options => options.UseSqlServer(mConnString2));

            // Repository Services
            services.AddTransient(typeof(ITraderRepository), typeof(TraderRepository));
            services.AddTransient(typeof(ISmartGridRepository), typeof(SmartGridRepository));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //var connection = @"Server=(localdb)\mssqllocaldb;Database=Personkartotek;Trusted_Connection=True;ConnectRetryCount=0";
            //services.AddDbContext<PersonContext>
            //    (options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // specifying the Swagger JSON endpoint.
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HandIn4 API V1");
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc();
        }
    }
}
