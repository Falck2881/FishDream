using FishDream.Components;
using FishDream.Helpers;
using FishDream.Sources;
using Microsoft.EntityFrameworkCore;

namespace FishDream
{
    public class Startup
    {
        // Этот метод регистрирует различные сервисы для нашего приложения.  
        // Это могут быть интерфейсы(поведение) или подключение к базе данных.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDbContext<CookingContext>(ConfigurationContextOptionBuilder);
            services.AddRazorComponents().
                AddInteractiveWebAssemblyComponents();

        }

        // Взаимодействие с .NET CORE проходит через HTTP запросы, прежде чем дойти до Controller , запросы проходят через middleware.
        // Middleware работают как фильтры где выполняется различная проверка на разную валидность, валидность зависит от требований.  
        // Здесь определяется какие связующие программные компоненты и для какой среды, будут использоваться в данном приложении.
        // Без которых не работало бы наше приложение. 
        // Также здесь определяються контроллеры. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseEndpoints(endpoint => endpoint.AddBlazorRender().MapControllers() );
        }

        private static void ConfigurationContextOptionBuilder(DbContextOptionsBuilder contextOptionBuilder)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");

            var config = configBuilder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
     
            contextOptionBuilder.UseNpgsql(connectionString); ;
        }

    }
}
