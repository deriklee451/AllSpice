

namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;

    private readonly Auth0Provider _auth0provider;

    private readonly IngredientService _ingredientService;

    private readonly FavoritesService _favoritesService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0provider, IngredientService ingredientService, FavoritesService favoritesService)
    {
        _recipesService = recipesService;
        _auth0provider = auth0provider;
        _ingredientService = ingredientService;
        _favoritesService = favoritesService;
    }


    [HttpGet]
    public ActionResult<List<Recipe>> Get()
    {
        try
        {
            // Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            List<Recipe> recipes = _recipesService.Get();
            return Ok(recipes);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.Create(recipeData);
            recipe.Creator = userInfo;
            return Ok(recipe);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }


    [HttpGet("{id}")]

    public async Task<ActionResult<Recipe>> GetOne(int id)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _recipesService.GetOne(id, userInfo?.Id);
            return Ok(recipe);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }


    [HttpPut("{id}")]

    public ActionResult<Recipe> Update([FromBody] Recipe recipeUpdate, int id)
    {
        try
        {
            Recipe recipe = _recipesService.Update(recipeUpdate, id);
            return Ok(recipe);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            throw;
        }
    }





}