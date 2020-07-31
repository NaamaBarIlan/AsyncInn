using Lab12_Relational_DB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12_Relational_DB.Model
{
    public class RoleInitializer
    {
        // Create a list of identity roles:

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.DistrictManager, NormalizedName 
                = ApplicationRoles.DistrictManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole{Name = ApplicationRoles.PropertyManager, NormalizedName 
                = ApplicationRoles.PropertyManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole{Name = ApplicationRoles.Agent, NormalizedName 
                = ApplicationRoles.Agent.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
        };

        public static void SeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> users, IConfiguration _config)
        {
            using (var dbContext = new AsyncInnDbContext(serviceProvider.GetRequiredService<DbContextOptions<AsyncInnDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
                SeedUsers(users, _config);
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
            if(userManager.FindByEmailAsync(_config["DistrictManagerEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["DistrictManagerEmail"];
                user.Email = _config["DistrictManagerEmail"];
                user.FirstName = "Jean";
                user.LastName = "Grey";

                IdentityResult result = userManager.CreateAsync(user, _config["DistrictManagerPassword"]).Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.DistrictManager).Wait();
                }
            }

            if (userManager.FindByEmailAsync(_config["PropertyManagerEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["PropertyManagerEmail"];
                user.Email = _config["PropertyManagerEmail"];
                user.FirstName = "Anna-Marie";
                user.LastName = "LeBeau";

                IdentityResult result = userManager.CreateAsync(user, _config["PropertyManagerPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.PropertyManager).Wait();
                }
            }

            if (userManager.FindByEmailAsync(_config["AgentEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["AgentEmail"];
                user.Email = _config["AgentEmail"];
                user.FirstName = "Danielle";
                user.LastName = "Moonstar";

                IdentityResult result = userManager.CreateAsync(user, _config["AgentPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.Agent).Wait();
                }
            }

        }

        private static void AddRoles(AsyncInnDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }

    }
}
