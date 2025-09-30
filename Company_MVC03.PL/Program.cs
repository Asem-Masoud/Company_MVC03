using Company_MVC03.BLL.Interfaces;
using Company_MVC03.BLL.Repositories;
using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.PL.Services;
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
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow DI For DepartmentRepository (Manually)
            // S04V09
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
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

            // app.UseAuthorization();

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

    // 09 -> 2:16
}
