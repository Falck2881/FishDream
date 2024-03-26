using System.ComponentModel.DataAnnotations.Schema;

namespace FishDream.Sources;

[Table("CookingRecipe")]
public class Recipe
{
    public int Index { get; set; }

    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string TypeBlude { get; set; } = null!;

    public int Time { get; set; }

    public string[] Recipes { get; set; }

    public string Path { get; set; } = null!;

    public string Guid { get; set; } = null!;

}
