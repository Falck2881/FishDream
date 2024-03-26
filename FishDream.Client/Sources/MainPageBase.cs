using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Linq;
using FishDream.Client.Helpers;

namespace FishDream.Client.Sources
{
    public class MainPageBase: ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; } = null!;

        protected bool IsFocusInput = false;

        protected string NameRecipe = "";

        protected string InputBackgroundStyle = "back-color-main-page-input";

        protected string MarginCardForInputRecipe = "margin-card-for-input-recipe-start";

        protected bool IsInputDisabled = true;

        protected bool IsRecipesScrollFeedEnabled = false;

        private List<Recipe> LoadedRecipes = new List<Recipe>();

        protected List<Recipe> FoundRecipesForShow = new List<Recipe>();

        protected List<string> RecipeNames = new List<string>();

        protected List<string> RecipeNamesContainingInputSubstring = new List<string>();

        private void UpdateStateVariableBeforeDisplayRecipesInScrollBar()
        {
            IsRecipesScrollFeedEnabled = true;
            MarginCardForInputRecipe = "margin-card-for-input-recipe-end";
        }

        private void FindRecipesContainingInputSubstring() =>
            FoundRecipesForShow = LoadedRecipes.Where(recipe => recipe.Name.Contains(NameRecipe)).ToList();

        private void UpdateMainPageAfterLoadRecipeNames()
        {
            IsInputDisabled = false;
            InputBackgroundStyle = "";
            StateHasChanged();
        }

        protected void ClearScrollbarFromFoundRecipes()
        {
            MarginCardForInputRecipe = "margin-card-for-input-recipe-start";
            IsRecipesScrollFeedEnabled = false;
            FoundRecipesForShow.Clear();
        }

        protected void DisplayDropDownListRecipeNames(MouseEventArgs eve)
        {
            if (eve.Button == 0)
            {
                if (NameRecipe != "")
                {
                    UpdateStateVariableBeforeDisplayRecipesInScrollBar();
                    FindRecipesContainingInputSubstring();
                }
            }
        }

        protected void DisplayDropDownListRecipeNames(KeyboardEventArgs eve)
        {
            if (eve.Code == "Enter" || eve.Code == "NumpadEnter")
            {
                if (NameRecipe != "")
                {
                    UpdateStateVariableBeforeDisplayRecipesInScrollBar();
                    FindRecipesContainingInputSubstring();
                }
            }
        }

        protected async Task LoadAllRecipes()
        {
            using var response = await Http.GetAsync("Cooking/FindNames");

            if (response.IsSuccessStatusCode)
            {
                LoadedRecipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();
                RecipeNames = LoadedRecipes.GetRecipeNames();
                RecipeNamesContainingInputSubstring = RecipeNames;
            }
            else
            {
                var message = $"{await response.Content.ReadAsStringAsync()}|{response.Content.ToString()}";
                throw new Exception(message);
            }

            UpdateMainPageAfterLoadRecipeNames();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await LoadAllRecipes();
        }

        protected void FocusIn() => IsFocusInput = true;

        protected void FocusOut() => IsFocusInput = false;

        protected void ConfirmSelectionInDropDownList(string nameRecipe)
        {
            NameRecipe = nameRecipe;
            IsFocusInput = false;
        }

        protected void DisplayDropDownListRecpeNamesContainingInputSubstring()
        {
            RecipeNamesContainingInputSubstring = RecipeNames;
            RecipeNamesContainingInputSubstring = RecipeNamesContainingInputSubstring.Where(recipe => recipe.Contains(NameRecipe)).ToList();
            IsFocusInput = true;
            StateHasChanged();
        }

        protected string GetAllIngredients(Recipe recipe)
        {
            var ingredients = "";

            foreach (var nameIngredient in recipe.Recipes)
            {
                ingredients += nameIngredient + ",";
            }

            return ingredients;
        }
    }
}
