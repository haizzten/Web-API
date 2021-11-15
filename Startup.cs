using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using f7.Services;
using f7.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc.Razor;

namespace f7
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
            // services.AddControllersWithViews();

            services.AddControllers();

            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(Configuration.GetSection("EmailSenderInfo"));

            services.AddDbContext<f7DbContext>(opt =>
                {
                    string connection = Configuration["ConnectionStrings:SqlConString"];
                    opt.UseSqlServer(connection);
                });

            services.AddDefaultIdentity<f7AppUser>(option =>
                {
                    option.SignIn.RequireConfirmedEmail = true;

                    option.Password.RequiredLength = 8;
                    option.Password.RequireUppercase = true;
                    option.Password.RequireDigit = true;

                    option.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 1, 0);
                    option.Lockout.MaxFailedAccessAttempts = 2;
                })
                .AddEntityFrameworkStores<f7DbContext>();


            services
                .AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    options.CallbackPath = "/Identity/Account/ExternalLogin/callback";
                })
                .AddFacebook(options =>
                {
                    options.AppId = Configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                    options.CallbackPath = "/signin-fb";
                });

            services.Configure<RazorViewEngineOptions>(options =>
                {
                    // options.ViewLocationFormats.Add("Areas/Views/{0}.cshtml");
                    // options.AreaPageViewLocationFormats.Add("Areas/Views/{0}.cshtml");
                    options.ViewLocationExpanders.Add(new MyViewLocationExpander());
                });

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapControllerRoute(
                // name: "default",
                // pattern: "{controller=Home}/{action=Index}/{id?}"
                // );
            }
            );
        }
    }
}
