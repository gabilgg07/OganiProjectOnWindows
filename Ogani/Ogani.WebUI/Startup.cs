using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ogani.WebUI.AppCode.Middlewares;
using Ogani.WebUI.AppCode.Providers;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity.Membership;

namespace Ogani.WebUI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(cfg =>
            {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));
            });


            services.AddDbContext<OganiDbContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("cString"));
                //cfg.UseSqlServer(Configuration.GetConnectionString("cStringMac"));
                //cfg.UseSqlServer(Configuration.GetConnectionString("cStringWindows"));
                //cfg.UseSqlServer(Configuration.GetConnectionString("cStringWindowsOganiAz"));
                //cfg.UseMySql(Configuration.GetConnectionString("cStringMySql"));
            });

            services.AddIdentity<OganiUser, OganiRole>()
                .AddEntityFrameworkStores<OganiDbContext>();

            services.AddScoped<SignInManager<OganiUser>>();
            services.AddScoped<UserManager<OganiUser>>();
            services.AddScoped<RoleManager<OganiRole>>();

            services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });

            services.AddAuthentication();
            services.AddAuthorization(cfg =>
            {

                if (Program.policies != null)
                {
                    foreach (var policy in Program.policies)
                    {
                        cfg.AddPolicy(policy, p =>
                        {
                            p.RequireAssertion(handler =>
                            {
                                bool allow = handler.User.IsInRole("SuperAdmin")
                                || handler.User.HasClaim(m => m.Type.Equals(policy) && m.Value.Equals("1"));

                                return allow;
                            });
                        });
                    }
                }
            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";

                cfg.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                cfg.Cookie.Name = "ogani";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Seed()
                .SeedMembership();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCultureMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuditMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("defaultSignin",
                    "Admin",
                    "{lang=az}/signin.html",
                    defaults: new
                    {
                        area = "admin",
                        controller = "account",
                        action = "signin"
                    },
                    constraints: new
                    {
                        lang = "az|en|ru"
                    });

                endpoints.MapAreaControllerRoute("defaultSignin",
                    "Admin",
                    "{lang=az}/accessdenied.html",
                    defaults: new
                    {
                        area = "admin",
                        controller = "account",
                        action = "accessdenied"
                    },
                    constraints: new
                    {
                        lang = "az|en|ru"
                    });

                endpoints.MapAreaControllerRoute("defaultArea", "Admin", "{lang=az}/admin/{controller=home}/{action=index}/{id?}",
                    constraints: new
                    {
                        lang = "az|en|ru"
                    });
                endpoints.MapControllerRoute("default", "{lang=az}/{controller=home}/{action=index}/{id?}",
                    constraints: new
                    {
                        lang = "az|en|ru"
                    });
            });
        }
    }
}

