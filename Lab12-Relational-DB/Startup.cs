using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model.Interfaces;
using Lab12_Relational_DB.Model.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lab12_Relational_DB
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // The appsettings.json, by default, is our "Configurations" for the app.
        // Set ourselves up for Dependency Injection
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // This is where all of our dependencies are going to live.
            // Enable the use of using controllers within the MVC convention
            // Instal-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson - Version 3.1.2
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // this is where all of our dependencies are going to live
            // Enable the use of using controllers within the MVC convention.
            // The below was removed after adding NewtonsoftJson
            //services.AddControllers();

            // Register with the app, that the DB exists, and what options to use for it. 
            services.AddDbContext<AsyncInnDbContext>(options =>
            {
                // Install package Microsoft.EntityFrameworkCore.SqlServer
                // Connection string = location where something lives, In our case, it's where our DB lives.
                // Connection string contains the location, username, pw of your sql server... with our sql database directly.
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // MAPPING - register my Dependency Injection Services
            services.AddTransient<IHotel, HotelRepository>();
            services.AddTransient<IRoom, RoomRepository>();
            services.AddTransient<IAmenity, AmenityRepository>();
            services.AddTransient<IHotelRoom, HotelRoomRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Set our default routing for our request within the API application
                // By default, our convention is {sit}/[controller]/[action]/[id]
                // id is not required, allowing it to be null-able
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
