using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

using Shop.Core.Data;
using Shop.Core.Services;
using Shop.Web.Helpers;
using Shop.Web.Middlewares;
using Shop.Web.Models;
using Shop.Web.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Shop.Web.Services;

namespace Shop.Web
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
            services.Configure<Settings>(Configuration);
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddDbContext<IdentityAppDbContext>(options =>
               options.UseMySql(this.Configuration.GetConnectionString("IdentityDatabase")));

            services.AddDefaultIdentity<IdentityUser>(
                options => {
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<IdentityAppDbContext>();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySql(this.Configuration.GetConnectionString("Database")));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IFormDataHelper, FormDataHelper>();

            services.AddAutoMapper(typeof(ViewMappingProfile));

            services.AddControllersWithViews();
           services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Information($"Connection string: {Configuration.GetConnectionString("Database")}");
            Log.Information($"Max count products: {Configuration["MaxCountProducts"]}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseMiddleware<ImageCacheMiddleware>(new ImageCacheOptions(env.WebRootPath + "/images", 5));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
