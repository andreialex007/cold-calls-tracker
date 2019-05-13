using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
using ColdCallsTracker.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ColdCallsTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AppControllerBase.AdminUserName = Configuration["AppSettings:AdminName"];
            AppControllerBase.AdminPassword = Configuration["AppSettings:AdminPassword"];

            //In-Memory
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
                    {
                        DateTimeFormat = "dd.MM.yyyy HH:mm"
                    };
                    options.SerializerSettings.Converters.Add(dateConverter);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            AppDbContext.ConnectionString = Configuration["AppSettings:MainConnnectionString"];

            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
                if (!db.States.Any())
                {
                    // db.States.Add(new State { Name = "В процессе" });
                    // db.States.Add(new State { Name = "Успех" });
                    // db.States.Add(new State { Name = "Отказ" });
                    // db.SaveChanges();
                }

                if (!db.Companies.Any())
                {
                    var companies = Enumerable.Range(1, 1000)
                        .Select(x => new Company
                        {
                            Name = "Company #" + x,
                            StateId = (x % 2) == 0 ? 1 : 2,
                            ActivityType = "Activity" + x,
                            Remarks = "Remarks" + x,
                            WebSites = "Websites"
                        }).ToList();
                    db.Companies.AddRange(companies);
                    db.SaveChanges();
                }

                if (!db.SystemSettings.Any())
                {
                    var systemSetting = new SystemSetting
                    {
                        Code = "ScriptText",
                        Value = "<em>This is script text</em>"
                    };
                    db.SystemSettings.Add(systemSetting);
                    db.SaveChanges();
                }

                if (!db.CostingTemplates.Any())
                {
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 1",
                        Qty = 3,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 2",
                        Qty = 4.5,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 3",
                        Qty = 2,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 4",
                        Qty = 1.5,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 5",
                        Qty = 1.5,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 6",
                        Qty = 1.5,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Costing template 7",
                        Qty = 2,
                        CategoryId = (int)CostingCategoryEnum.Category1
                    });
                   
                    db.SaveChanges();
                }

            }

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru-RU");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("ru-RU") };
            });

        }

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

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
