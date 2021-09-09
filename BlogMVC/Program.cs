using BlogMVC.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try {
                var scope = host.Services.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                var userMngr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMngr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                context.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");

                if (!context.Roles.Any())
                {
                
                    roleMngr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }


                if (!context.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com"


                    };

                    var result = userMngr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                    userMngr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }

            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            
            

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
