using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ColdCallsTracker.Code.Data;
using ColdCallsTracker.Code.Data.Models;
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

            AppDbContext.ConnectionString = Configuration.GetConnectionString("MainConnnectionString");

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
                        Name = "Лента с детальным просмотром и постраничностью",
                        Qty = 3,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Лента с детальным просмотром и постраничностью + сложный фильтр",
                        Qty = 4.5,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Форма из 5-ти инпутов",
                        Qty = 2,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Полнотекстовый поиск по сайту",
                        Qty = 1.5,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Страница со статической информацией",
                        Qty = 1.5,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Лента без детального просмотра",
                        Qty = 1.5,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Лента без детального просмотра с постраничностью",
                        Qty = 2,
                        CategoryId = (int)CostingCategoryEnum.Ui
                    });
                    db.CostingTemplates.Add(new CostingTemplate
                    {
                        Unit = (int)UnitEnum.Hours,
                        Name = "Интеграция с 1с базовая",
                        Qty = 5,
                        CategoryId = (int)CostingCategoryEnum.Integration
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
