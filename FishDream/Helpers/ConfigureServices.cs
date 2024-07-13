using Microsoft.AspNetCore.HttpOverrides;

namespace FishDream.Helpers
{
    public static class ConfigureHelper
    {
        
        /// <summary>
        /// // !!! Конфигурируем обработку пересылаемых заголовков запросов
        /// Указывает, что должны обрабатываться заголовки X-Forwarded-For и X-Forwarded-Proto. 
        /// Эти заголовки обычно используются в конфигурациях с реверс-прокси серверами для передачи информации о оригинальном запросе.
        /// </summary>
        /// <param name="options"></param>
        public static void ConfiguringProcessingForwardedRequestHeaders(ForwardedHeadersOptions options)
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        }
    }
}
