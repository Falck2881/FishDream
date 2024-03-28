using Microsoft.AspNetCore.HttpOverrides;

namespace FishDream.Helpers
{
    public static class ConfigureHelper
    {
        // !!! Конфигурируем обработку пересылаемых заголовков запросов
        public static void ConfiguringProcessingForwardedRequestHeaders(ForwardedHeadersOptions options)
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        }
    }
}
