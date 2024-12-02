using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ValleDeOro.Models;

namespace ValleDeOro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /*-----------------------------------------------------------------------------------------------------------------------------------------------*/

            builder.Services.AddDbContext<GvglampingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BloggingDatabase")));

            void ConfigureServices(IServiceCollection services)
            {
                //Mapper
                var mapperConfig = new MapperConfiguration(sun => {
                    sun.AddProfile(new MappingProfile());
                });
                IMapper mapper = mapperConfig.CreateMapper();
                services.AddSingleton(mapper);
                services.AddMvc();
            }

            builder.Services.AddControllers(options =>
            {
                options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
            });

            /*-----------------------------------------------------------------------------------------------------------------------------------------------*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
