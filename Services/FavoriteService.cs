namespace AllSpice.Services
{
    public class FavoriteService
    {

        private readonly FavoriteRepository _repo;

        public FavoriteService(FavoriteRepository repo)
        {
            _repo = repo;
        }

        internal Favorite GetById(int id)
        {
            Favorite found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("ID Not Found");
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
                throw new Exception("not yours to delete");
            }
            _repo.Delete(id);
        }

    }
}