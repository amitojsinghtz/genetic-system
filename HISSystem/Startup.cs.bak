using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Classes;
using Repository.Interfaces;
using Service.IServices;
using Service.Services;
using Service.UnitOfServices;

namespace HISSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    
                builder =>
                {
                    builder.AllowAnyOrigin();
                   
                });
            });
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //  services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPersonalInformation, PersonalInformationService>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<IBranchService, BranchService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IRoomService, RoomService>();
            services.AddTransient<IBedAllocationService, BedAllocationService>();
            services.AddTransient<IBuildingService, BuildingService>();
            services.AddTransient<IFloorService, FloorService>();
            services.AddTransient<IBedService, BedService>();
            services.AddTransient<IPatientAdmissionService, PatientService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IViewControlService, ViewControlService>();
            services.AddTransient<IUnitOfService, UnitOfService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseCors();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                     name: "areaRoute",
                    template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



        }
    }
}
