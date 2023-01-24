

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

    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ingredient = _repo.Create(ingredientData);
        return ingredient;
    }


    internal string Remove(int id, string userId)
    {
        Ingredient original = _repo.GetOne(id);

    }




}
