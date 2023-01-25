namespace AllSpice.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;

    private readonly RecipesService _recipesService;

    private readonly FavoriteService _favoritesService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider,
    FavoriteService favoriteService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _favoritesService = favoriteService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("favorites")]
    public async Task<ActionResult<List<FavoriteRecipes>>> GetFavorites()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<FavoriteRecipes> recipes = _favoritesService.GetFavoriteRecipes(userInfo.Id);
            return Ok(recipes);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }


}
