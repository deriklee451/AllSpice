namespace AllSpice.Repositories;

public class FavoriteRepository
{

    private readonly IDbConnection _db;

    public FavoriteRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<FavoriteRecipes> foundFavorite(string accountId)
    {
        string sql = @"
            SELECT
            r.*,
            f.*,
            a.*
            FROM recipes r
            JOIN favorites f ON f.recipeId = r.id
            JOIN accounts a ON f.accountId = a.id
            WHERE
            f.accountId = accountId;
            ";
        return _db.Query<FavoriteRecipes, Favorite, Account, FavoriteRecipes>(sql, (recipe, favorite, account) =>
        {
            recipe.FavoriteId = favorite.Id;
            recipe.Creator = account;
            return recipe;
        }, new { accountId }).ToList();
    }

    internal Favorite Create(Favorite favoriteData)
    {
        string sql = @"
            INSERT INTO favorites
            (accountId, recipeId)
            VALUES
            (@AccountId, @RecipeId);
            SELECT LAST_INSERT_ID();
            ";
        int id = _db.ExecuteScalar<int>(sql, favoriteData);
        favoriteData.Id = id;
        return favoriteData;
    }

    internal Favorite GetById(int id)
    {
        string sql = @"
            SELECT
            *
            FROM favorites
            WHERE id = @id
            ";
        return _db.QueryFirstOrDefault<Favorite>(sql, new { id });
    }

    internal void Delete(int id)
    {
        string sql = @"
            DELETE
            FROM
            favorites
            WHERE
            id = @id limit 1
            ";

        _db.Execute(sql, new { id });
    }



}
