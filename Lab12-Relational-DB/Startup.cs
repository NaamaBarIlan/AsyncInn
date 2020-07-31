using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab12_Relational_DB.Data;
using Lab12_Relational_DB.Model;
using Lab12_Relational_DB.Model.Interfaces;
using Lab12_Relational_DB.Model.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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
            services.AddControllers(options =>
            {
                //Make all routes by default autorized to require login:
                options.Filters.Add(new AuthorizeFilter());

            })
                .AddNewtonsoftJson(options =>
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

            //Register IDENTITY
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AsyncInnDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWTIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["JWTKey"]))

                };
            });


            // Add my policies:
            services.AddAuthorization(options =>
           {
               options.AddPolicy("GoldPrivileges", policy => policy.RequireRole(ApplicationRoles.DistrictManager));

               options.AddPolicy("SilverPrivileges", policy => policy.RequireRole(ApplicationRoles.DistrictManager, ApplicationRoles.PropertyManager));

               options.AddPolicy("BronzePrivileges", policy => policy.RequireRole(ApplicationRoles.DistrictManager, ApplicationRoles.PropertyManager, ApplicationRoles.Agent));

               //To add a policy based on a claim:
               ///options.AddPolicy("ColorPolicy", policy => policy.RequireClaim("FavColor"));
           });

            // MAPPING - register my Dependency Injection Services
            services.AddTransient<IHotel, HotelRepository>();
            services.AddTransient<IRoom, RoomRepository>();
            services.AddTransient<IAmenity, AmenityRepository>();
            services.AddTransient<IHotelRoom, HotelRoomRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Important! Auth has to be after UseRouting()!
            app.UseAuthentication();
            app.UseAuthorization();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            RoleInitializer.SeedData(serviceProvider, userManager, Configuration);

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
