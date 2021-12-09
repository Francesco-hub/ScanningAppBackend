using ScanningApp.Core.DomainService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScanningApp.Infrastructure.Data.Repositories;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.ApplicationService.Services;
using Microsoft.Extensions.Hosting;
using ScanningApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ScanningApp.Core.Entity;
using Newtonsoft.Json;
using WorkerService;

namespace ScanningAppBackend
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
            services.AddCors(o => o.AddPolicy("AllowEverything", builder =>
            
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                       .AllowAnyHeader()
            ));

            services.AddHostedService<Worker>();
            services.AddHttpClient();
            services.AddDbContext<ScanningAppContext>(
                opt => opt.UseSqlite("Data Source=sqlite.db"));
            /*services.AddDbContext<ScanningAppContext>(
                opt => opt.UseInMemoryDatabase("ConDb")
                );*/
            services.AddScoped<IConcertRepository, ConcertRepository>();
            services.AddScoped<IConcertService, ConcertService>();

            services.AddScoped<IScanRepository, ScanRepository>();
            services.AddScoped<IScanService, ScanService>();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowEverything");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<ScanningAppContext>();
                    DbInitializer.InitData(ctx);
                }
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
