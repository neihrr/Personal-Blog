using BlogMVC.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogMVC.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

//Yazýlan diðer cs dosyalarýnýn okunabilir ve iþlevli kýlýnmasýný saðlayan dosya

namespace BlogMVC
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
         {
             options.Password.RequireDigit = false;
             options.Password.RequireUppercase = false;
             options.Password.RequireLowercase = false;
             options.Password.RequireNonAlphanumeric = false;
             options.Password.RequireNonAlphanumeric = false;

         })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>();
             
            services.Configure<CookiePolicyOptions>(options => { options.CheckConsentNeeded = context => true;options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None; });
            //To override the redirect URL
            services.ConfigureApplicationCookie(options => { options.LoginPath = "/Auth/LogIn"; });
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(_config["DefaultConnection"]));
            services.AddTransient<IRepository, Repository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

           

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
