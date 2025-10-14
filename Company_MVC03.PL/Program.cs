using Company_MVC03.BLL;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.BLL.Repositories;
using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.DAL.Models;
using Company_MVC03.PL.MappingProfiles;
using Company_MVC03.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company_MVC03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Register Built-in MVC Services
            // V07
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow DI For DepartmentRepository (Manually)
            // S04V09
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Allow DI

            //V08
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });// Allow DI For CompanyDbContext (Manually)
               // V07




            /*
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {

                options.UseSqlServer("Server= PC_2001\\SQLEXPRESS; Database=CompanyDB03;Trusted_Connection=True;TrustServerCertificate=True;");// ->  Because we may be changed in it in the future so we copy it to "appsettings.json"
            });
            */

            /*// S04V05
            // Allow Dependances 
            // depend on Life Time
            builder.Services.AddScoped(); // Create Object Life Time Per Request - UnReachable Object
            builder.Services.AddTransient(); // Create Object Life Time Per Operation 
            builder.Services.AddSingleton(); // Create Object Life Time Per Application
            */

            builder.Services.AddScoped<IScopedService, ScopedServices>(); // 
            builder.Services.AddTransient<ITransientServices, TransientServices>(); //  
            builder.Services.AddSingleton<ISingletonServices, SingletonServices>(); // 

            // AutoMapper
            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>();

            builder.Services.ConfigureApplicationCookie(config => /*Because Used [Authorize] In HomeController*/
            {
                config.LoginPath = "/Account/SignIn";

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            #region SignIn&SignUp

            app.UseAuthorization();
            app.UseAuthorization();

            #endregion

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

            // VO8 
            // in Package Manager Console -> install-package Microsoft.EntityFrameworkCore.Tools
            // Add-Migration "InitialCreate" -OutputDir Data/Migrations // to create Migration in Data/Migrations folder (Default Project must be DAL) in this line
            // Update-Database // to create Database from Migration (Default Project must be DAL) in this line


        }
    }
}

#region S06V06 Asynch (Parallel) VS Sync(Series)
// Synchronous (Series) : Each Statement Wait The Previous Statement To Be Completed
// VS Asynchronous (Parallel) : Each Statement Do Not Wait The Previous Statement To Be Completed
class Test
{
    public void Fun01()
    {
        // Statement01
        // Statement02
        // await // Statement03 -> Take Time
        // Statement04
        // Statement05
    }
    public void Fun02()
    {
        // Statement01
        // Statement02
        // Statement03
        // Statement04
        // Statement05
    }
}

#endregion