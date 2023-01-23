
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
    (title, instructions, img, category)
    VALUES
    (@title, @instructions, @img, @category)
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

        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;

            return recipe;
        }).ToList();
        return recipes;
    }




}
