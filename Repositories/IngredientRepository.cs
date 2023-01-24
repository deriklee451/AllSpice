

namespace AllSpice.Repositories;

public class IngredientRepository
{
    private readonly IDbConnection _db;

    public IngredientRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@name, @quantity, @recipeId)
            SELECT LAST_INSERT_ID();
            ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }

}
