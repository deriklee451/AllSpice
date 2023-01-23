namespace AllSpice.Services;

public class RecipesService
{

    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }


    internal List<Recipe> Get()
    {
        List<Recipe> recipes = _repo.Get();

        // NOTE This might be useful come friday
        // List<Recipe> filtered = recipes.FindAll(r => r.Archived == false || r.CreatorId == userId);

        return recipes;
    }

    internal Recipe Create(Recipe recipeData)
    {
        Recipe recipe = _repo.Create(recipeData);

        return recipe;
    }

}
