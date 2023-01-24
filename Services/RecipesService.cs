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

    internal Recipe GetOne(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null)
        {
            throw new Exception("No recipe at that ID");
        }
        return recipe;
    }

    internal Recipe Update(Recipe recipeUpdate, int id, string userId)
    {
        Recipe original = GetOne(id, userId);
        original.Title = recipeUpdate.Title ?? original.Title;
        original.Instructions = recipeUpdate.Instructions ?? original.Instructions;
        original.Img = recipeUpdate.Img ?? original.Img;
        original.Category = recipeUpdate.Category ?? original.Category;

        bool edited = _repo.Update(original);
        if (edited == false)
        {
            throw new Exception("Recipe Not Edited");
        }
        return original;
    }

    internal string Remove(int id, string userId)
    {
        Recipe recipe = GetOne(id, userId);
        _repo.Remove(id);


        return $"Recipe was deleted";

    }




}


