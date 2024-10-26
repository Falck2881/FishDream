using FishDream.Helpers;
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
            // !!! Конфигурируем обработку пересылаемых заголовков запросов
            services.Configure<ForwardedHeadersOptions>(ConfigureHelper.ConfiguringProcessingForwardedRequestHeaders);

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

            // Использовать когда только будет SSL сертификат 
            // app.UseHttpsRedirection();
            // !!! Добавляем в конвеер обработки HTTP-запроса компонент работы с пересылаемыми заголовками
            app.UseForwardedHeaders();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseEndpoints(endpoint => endpoint.AddBlazorRender().MapControllers() );
        }

        private static void ConfigurationContextOptionBuilder(DbContextOptionsBuilder contextOptionBuilder)
        {
            var configBuilder = new ConfigurationBuilder();
            //- Устанавливает базовый путь для конфигурации в текущий рабочий каталог приложения.
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            // - Добавляет файл конфигурации appsettings.json, который содержит параметры подключения и другие настройки приложения.
            configBuilder.AddJsonFile("appsettings.json");
            
            //- Строит конфигурацию и создает объект IConfiguration, который содержит все параметры из конфигурационных файлов и других источников.
            var config = configBuilder.Build();
            //  - Получает строку подключения к базе данных из конфигурации под именем "DefaultConnection
            var connectionString = config.GetConnectionString("DefaultConnection");
     
            //- Настраивает контекст базы данных для использования PostgreSQL в качестве базы данных, используя строку подключения
            contextOptionBuilder.UseNpgsql(connectionString); ;
        }

    }
}
