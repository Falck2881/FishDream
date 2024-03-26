using Microsoft.AspNetCore.Mvc;
using FishDream.Helpers;
using FishDream.Sources;

namespace FishDream.Controllers
{
    [ApiController]
    [Route("Cooking")]
    public class CookingRecipesController: ControllerBase
    {
        private CookingContext _cookingContext;

        public CookingRecipesController(CookingContext cookingContext)
        {
            _cookingContext = cookingContext;
        }

        [HttpGet]
        [Route("FindNames")]
        public IEnumerable<Recipe> FindAllNamesRecipes() =>
            _cookingContext.Entries.ToList();
        
        
    }
}
