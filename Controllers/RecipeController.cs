

namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly RecipesService _recipesService;

    private readonly Auth0Provider _auth0provider;

    private readonly IngredientService _ingredientService;

    private readonly FavoritesService _favoritesService;

    public RecipeController(RecipesService recipesService, Auth0Provider auth0provider, IngredientService ingredientService, FavoritesService favoritesService)
    {
        _recipesService = recipesService;
        _auth0provider = auth0provider;
        _ingredientService = ingredientService;
        _favoritesService = favoritesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Recipe>>> Get()
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipes = _recipesService.Get(userInfo?.Id);
            return Ok(recipes);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }




}