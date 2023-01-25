namespace AllSpice.Repositories;

public class FavoriteRepository
{

    private readonly IDbConnection _db;

    public FavoriteRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite foundFavorite(Favorite favoriteData)
    {
        string sql = @"
            SELECT
            *
            FROM favorite
            WHERE
            recipeId = @RecipeId
            AND
            accountId = @AccountId
            ";
        return _db.QueryFirstOrDefault<Favorite>(sql, favoriteData);
    }

    internal Favorite Create(Favorite favoriteData)
    {
        string sql = @"
            INSERT INTO favorite
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
            FROM favorite
            WHERE id = @id
            ";
        return _db.QueryFirstOrDefault<Favorite>(sql, new { id });
    }

    internal void Delete(int id)
    {
        string sql = @"
            DELETE
            FROM
            favorite
            WHERE
            id = @id limit 1
            ";

        _db.Execute(sql, new { id });
    }



}
