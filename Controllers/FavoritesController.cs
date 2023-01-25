namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{

    private readonly FavoriteService _favoriteService;

    private readonly Auth0Provider _auth0Provider;

    public FavoritesController(FavoriteService favoriteService, Auth0Provider auth0Provider)
    {
        _favoriteService = favoriteService;
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
            Favorite newFavorite = _favoriteService.Create(favoriteData);
            return Ok(newFavorite);
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
            Favorite favorite = _favoriteService.GetById(id);
            return Ok(favorite);
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
            _favoriteService.Delete(id, userInfo.Id);
            return Ok("Deleted");
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

}
