
namespace FishDream.Client.Helpers
{
    public static class RecipeHelpers
    {
        public static List<string> GetRecipeNames(this IEnumerable<Recipe> allRecipes)
        {
            var allNameRecipesWithNumberVersions = new List<string>();

            foreach(var recipe in allRecipes)
            {
                // name - в коллекции allNameRecipes может дублироваться, проверяем было ли ранее добавлено имя, чтобы не возводить в N степень дублирование
                if (allNameRecipesWithNumberVersions.Contains(recipe.Name))
                    continue;

                // Ищем все дубликаты и запоминаем
                allNameRecipesWithNumberVersions.Add(recipe.Name);
            }

            return allNameRecipesWithNumberVersions;
        }
    }
}
