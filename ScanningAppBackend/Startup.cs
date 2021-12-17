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
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {            
            Configuration = configuration;
            Environment = env;
            //JwtSecurityKey.SetSecret("nnfal45lngfqLqLLLLL75K");
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
    
            services.AddDbContext<ScanningAppContext>(opt => opt.UseSqlite("Data Source=sqlite.db"));


            services.AddCors(o => o.AddPolicy("AllowEverything", builder =>
            
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
            ));

            services.AddHttpClient();
            

            services.AddScoped<IConcertRepository, ConcertRepository>();
            services.AddScoped<IConcertService, ConcertService>();

            services.AddScoped<IScanRepository, ScanRepository>();
            services.AddScoped<IScanService, ScanService>();
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddMvc(options => options.EnableEndpointRouting = false);

           services.AddHostedService<Worker>();
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
                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();
                    //DbInitializer.InitData(ctx);
                }
            }
            else //Production
            {

                using(var scope = app.ApplicationServices.CreateScope())
                {
                   var ctx = scope.ServiceProvider.GetService<ScanningAppContext>();
                    app.UseExceptionHandler("/Home/Error");
                    ctx.Database.EnsureCreated();
                }
            }
          

            //app.UseHttpsRedirection();
           // app.UseMvc();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
