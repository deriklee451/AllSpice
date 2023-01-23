
namespace AllSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;

    }


    internal Recipe Create(Recipe recipeData)
    {
        string sql = @"
    INSERT INTO recipes
    (title, instructions, img, category, creatorId)
    VALUES
    (@title, @instructions, @img, @category, @creatorId);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, recipeData);
        recipeData.Id = id;
        return recipeData;
    }


    internal List<Recipe> Get()
    {
        string sql = @"
        SELECT
        rc.*,
        ac.*
        FROM recipes rc
        JOIN accounts ac ON ac.id = rc.creatorId;
        ";

        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
        {
            recipe.Creator = profile;

            return recipe;
        }).ToList();
        return recipes;
    }




}
