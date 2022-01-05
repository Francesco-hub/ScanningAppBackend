using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.ApplicationService.Services;
using ScanningApp.Core.DomainService;
using ScanningApp.Infrastructure.Data;
using ScanningApp.Infrastructure.Data.Repositories;
using WorkerService;

namespace ScanningAppBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {            
            Configuration = configuration;
            Environment = env;
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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

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
                    DbInitializer.InitMockData(ctx);
                    DbInitializer.InitializeUsers(ctx);
                }
            }
            //Production
            else
            {

                using(var scope = app.ApplicationServices.CreateScope())
                {
                   var ctx = scope.ServiceProvider.GetService<ScanningAppContext>();
                   app.UseExceptionHandler("/Home/Error");
                   ctx.Database.ExecuteSqlRaw("DELETE * FROM USERS");
                   ctx.Database.EnsureCreated();
                   //DbInitializer.InitializeUsers(ctx);
                }
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
