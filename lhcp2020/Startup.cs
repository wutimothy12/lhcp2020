using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using lhcp2020.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace lhcp2020
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:lhcp2020Identity:ConnectionString"]));

            services.AddDbContext<lhcp2020Context>(options =>
                options.UseSqlServer(
                    Configuration["Data:lhcp2020Products:ConnectionString"]));
            services.AddTransient<ChinesePaintingQueries>();

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
             "EmailClub",                                              // Route name
             "EmailClub",                           // URL with parameters
             new { controller = "MailRecipients", action = "Create" }  // Parameter defaults
             );

                routes.MapRoute(
              "exhibits",                                              // Route name
              "all-paintings",                           // URL with parameters
              new { controller = "ChinesePainting", action = "Index", page = 1 }  // Parameter defaults
              );

                routes.MapRoute(
                    "exhibitsByPage",
                    "exhibits/page{page}",
                    new { controller = "ChinesePainting", action = "Index", category = (string)null, page = (int?)null },
                    new { page = "[0-9]+" }
                );

                routes.MapRoute(
                  "exhibitsByDate",                                              // Route name
                  "exhibits/chinese-new-arrival-paintings",                           // URL with parameters
                  new { controller = "ChinesePainting", action = "bydate", page = 1 }  // Parameter defaults
              );

                routes.MapRoute(
                  "exhibitsByDatePage",                                              // Route name
                  "exhibits/chinese-new-arrival-paintings/page{page}",                           // URL with parameters
                  new { controller = "ChinesePainting", action = "bydate", page = (int?)null },  // Parameter defaults
                  new { page = "[0-9]+" }
              );

                routes.MapRoute(
                   "priceA",                                              // Route name
                   "exhibits/price-under-50",                           // URL with parameters
                   new { controller = "ChinesePainting", action = "bypriceA", page = 1 }  // Parameter defaults
               );

                routes.MapRoute(
                    "priceAByPage",
                    "exhibits/price-under-50/page{page}",
                    new { controller = "ChinesePainting", action = "bypriceA", category = (string)null, page = (int?)null },
                    new { page = "[0-9]+" }
                );

                routes.MapRoute(
                  "priceB",                                              // Route name
                  "exhibits/price-between-50-to-200",                           // URL with parameters
                  new { controller = "ChinesePainting", action = "bypriceB", page = 1 }  // Parameter defaults
              );

                routes.MapRoute(
                    "priceBByPage",
                    "exhibits/price-between-50-to-200/page{page}",
                    new { controller = "ChinesePainting", action = "bypriceB", category = (string)null, page = (int?)null },
                    new { page = "[0-9]+" }
                );

                routes.MapRoute(
                 "priceC",                                              // Route name
                 "exhibits/price-between-200-to-500",                           // URL with parameters
                 new { controller = "ChinesePainting", action = "bypriceC", page = 1 }  // Parameter defaults
             );

                routes.MapRoute(
                    "priceCByPage",
                    "exhibits/price-between-200-to-500/page{page}",
                    new { controller = "ChinesePainting", action = "bypriceC", category = (string)null, page = (int?)null },
                    new { page = "[0-9]+" }
                );

                routes.MapRoute(
                  "priceD",                                              // Route name
                  "exhibits/price-over-50",                           // URL with parameters
                  new { controller = "ChinesePainting", action = "bypriceD", page = 1 }  // Parameter defaults
              );

                routes.MapRoute(
                    "priceDByPage",
                    "exhibits/price-over-50/page{page}",
                    new { controller = "ChinesePainting", action = "bypriceD", category = (string)null, page = (int?)null },
                    new { page = "[0-9]+" }
                );

                routes.MapRoute(
                  "exhibitsByName",                                              // Route name
                  "exhibits/chinese-{name}-paintings",                           // URL with parameters
                  new { controller = "ChinesePainting", action = "byname", name = (string)null, page = 1 },  // Parameter defaults
                  new { name = "[a-zA-z0-9\\-]+" }
              );

                routes.MapRoute(
                 "exhibitsByNamePage",                                              // Route name
                 "exhibits/chinese-{name}-paintings/page{page}",                           // URL with parameters
                 new { controller = "ChinesePainting", action = "byname", name = (string)null, page = (int?)null },  // Parameter defaults
                 new { name = "[a-zA-z0-9\\-]+", page = "[0-9]+" }
             );

                routes.MapRoute(
                   "exhibitsByType",                                              // Route name
                   "exhibits/chinese-paintings-type-of-{name}",                           // URL with parameters
                   new { controller = "ChinesePainting", action = "bytype", name = (string)null, page = 1 },  // Parameter defaults
                   new { name = "[a-zA-z0-9\\-]+" }
               );

                routes.MapRoute(
                 "exhibitsByTypePage",                                              // Route name
                 "exhibits/chinese-paintings-type-of-{name}/page{page}",                           // URL with parameters
                 new { controller = "ChinesePainting", action = "bytype", name = (string)null, page = (int?)null },  // Parameter defaults
                 new { name = "[a-zA-z0-9\\-]+", page = "[0-9]+" }
             );


                routes.MapRoute(
                   "exhibitsByCategory",                                              // Route name
                   "exhibits/chinese-paintings-of-{category}",                           // URL with parameters
                   new { controller = "ChinesePainting", action = "bycategory", category = (string)null, page = 1 },  // Parameter defaults
                   new { category = "[a-zA-z0-9\\-]+" }
               );

                routes.MapRoute(
                  "exhibitsByCategoryPage",                                              // Route name
                  "exhibits/chinese-paintings-of-{category}/page{page}",                           // URL with parameters
                  new { controller = "ChinesePainting", action = "bycategory", category = (string)null, page = (int?)null },  // Parameter defaults
                  new { category = "[a-zA-z0-9\\-]+", page = "[0-9]+" }
              );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
