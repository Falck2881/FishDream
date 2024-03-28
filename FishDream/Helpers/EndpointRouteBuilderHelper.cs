using FishDream.Components;
using Microsoft.AspNetCore.Mvc;

namespace FishDream.Helpers
{
    public static class EndpointRouteBuilderHelper
    {
        public static IEndpointRouteBuilder AddBlazorRender(this IEndpointRouteBuilder endpoint) 
        {
            endpoint.MapRazorPages();
            endpoint.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(new[] { typeof(Client._Imports).Assembly });

            return endpoint;
        }

        public static IEndpointRouteBuilder AddRouteToControllers(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapControllerRoute(name: "default", pattern: "{controller=CookingRecipes}/{action=FindRecipes}");

            return endpoint;
        }
    }
}
