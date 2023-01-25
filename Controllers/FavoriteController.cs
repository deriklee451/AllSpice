namespace Allspice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly FavoritesService _favoritesService;

        private readonly Auth0Provider _auth0Provider;

        public FavoriteController(FavoritesService favoritesService, Auth0Provider auth0Provider)
        {
            _favoritesService = favoritesService;
            _auth0Provider = auth0Provider;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                favoriteData.AccountId = userInfo.Id;
                Favorite newFavorite = _favoritesService.Create(favoriteData);
                return Ok(newFavorite);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Favorite>> Delete(int id)
        {
            try
            {
                Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
                _favoritesService.Delete(id, userInfo.Id);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Favorite> Get(int id)
        {
            try
            {
                Favorite favorite = _favoritesService.GetById(id);
                return Ok(favorite);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}