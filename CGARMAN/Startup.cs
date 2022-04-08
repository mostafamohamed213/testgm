using AutoMapper;
using CGARMAN.Models;
using CGARMAN.Services;
using CGARMAN.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.IRepositories;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF;

using RepositoryPatternWithUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CGARMAN
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
            services.AddControllersWithViews();
            services.AddDbContext<WMSContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("wms"),
                  b => b.MigrationsAssembly(typeof(WMSContext).Assembly.FullName)));

            //  services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<WMSContext>();        

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

            })
    .AddEntityFrameworkStores<WMSContext>()
    .AddDefaultTokenProviders();

            // services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddSession();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<InventoryServices, InventoryServices>();
            services.AddTransient<TechniciansServices, TechniciansServices>();
            services.AddTransient<CompanyServices, CompanyServices>();
            services.AddTransient<ShiftServices, ShiftServices>();
            services.AddTransient<PositionServices, PositionServices>();
            services.AddTransient<CostCenterServices, CostCenterServices>();
            services.AddTransient<TechnicianAttendanceServices, TechnicianAttendanceServices>();
            services.AddTransient<TireSizeServices, TireSizeServices>(); 
            services.AddTransient<VehicleStatusServices, VehicleStatusServices>();
            services.AddTransient<VehicleDepartmentServices, VehicleDepartmentServices>();
            services.AddTransient<VehicleOwnerServices, VehicleOwnerServices>();
            services.AddTransient<VehicleFamilyServices, VehicleFamilyServices>(); 
            services.AddTransient<VehicleBrandServices, VehicleBrandServices>();
            services.AddTransient<VehicleServices, VehicleServices>();

            services.AddAutoMapper(typeof(Startup));
            //services.AddTransient(provider => new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new MappingProfile(provider.GetService<UnitOfWork>()));
            //}).CreateMapper());
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/login";
                options.LogoutPath = $"/Account/logout";
                options.AccessDeniedPath = $"/Account/accessDenied";
                //  options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
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
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
