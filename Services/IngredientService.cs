

namespace AllSpice.Services;

public class IngredientService
{
    private readonly IngredientRepository _repo;

    private readonly RecipesService _recipeService;

    public IngredientService(IngredientRepository repo, RecipesService recipeService)
    {
        _repo = repo;
        _recipeService = recipeService;
    }


    internal Ingredient Create(Ingredient ingredientData, string userId)
    {
        Recipe found = _recipeService.GetOne(ingredientData.RecipeId);
        if (found.CreatorId != userId)
        {
            throw new Exception("No recipe");
        }
        _repo.Create(ingredientData);
        return (ingredientData);

    }

    internal Ingredient GetById(int id)
    {
        Ingredient found = _repo.GetById(id);
        if (found == null)
        {
            throw new Exception("No recipe at this ID");
        }
        return found;
    }

    internal List<Ingredient> GetByRecipeId(int recipeId)
    {
        _recipeService.GetOne(recipeId);
        return _repo.GetByRecipeId(recipeId);
    }

    internal Ingredient Edit(Ingredient ingredientData, string userId)
    {
        Ingredient original = this.GetById(ingredientData.Id);
        Recipe found = _recipeService.GetOne(original.RecipeId);
        if (found.CreatorId != userId)
        {
            throw new Exception("Not Your Recipe");
        }
        original.Name = ingredientData.Name ?? original.Name;
        original.Quantity = ingredientData.Quantity ?? original.Quantity;
        _repo.Edit(original);
        return original;
    }

    internal Ingredient Delete(int id, string userId)
    {
        Ingredient original = this.GetById(id);
        Recipe found = _recipeService.GetOne(original.RecipeId);
        if (found.CreatorId != userId)
        {
            throw new Exception("This is not your to alter.");
        }
        _repo.Delete(original);
        return original;
    }
}