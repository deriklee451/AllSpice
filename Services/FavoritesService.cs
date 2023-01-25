
namespace Allspice.Services;

public class FavoritesService
{
    private readonly FavoriteRepository _repo;

    public FavoritesService(FavoriteRepository repo)
    {
        _repo = repo;
    }

    internal Favorite GetById(int id)
    {
        Favorite found = _repo.GetById(id);
        if (found == null)
        {
            throw new Exception("Invalid Id");
        }
        return found;
    }

    internal Favorite Create(Favorite favoriteData)
    {
        Favorite found = _repo.foundFavorite(favoriteData);
        if (found != null)
        {
            return found;
        }
        return _repo.Create(favoriteData);
    }

    internal void Delete(int id, string userId)
    {
        Favorite toDelete = GetById(id);
        if (toDelete.AccountId != userId)
        {
            throw new Exception("Unable to delete");
        }
        _repo.Delete(id);
    }
}
