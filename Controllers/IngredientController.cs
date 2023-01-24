
namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientController : ControllerBase
{
    private readonly IngredientService _ingredientService;

    private readonly Auth0Provider _auth0Provider;

    public IngredientController(IngredientService ingredientService, Auth0Provider auth0Provider)
    {
        _ingredientService = ingredientService;
        _auth0Provider = auth0Provider;

    }



    [HttpGet("{id}")]
    public ActionResult<Ingredient> Get(int id)
    {
        try
        {
            Ingredient ingredient = _ingredientService.GetById(id);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Ingredient ingredient = _ingredientService.Create(ingredientData, userInfo.Id);
            return Ok(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> EditAsync(int id, [FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            ingredientData.Id = id;
            Ingredient update = _ingredientService.Edit(ingredientData, userInfo.Id);
            //remember to return so you do not get an error above on EditAsync. 
            return Ok(update);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> DeleteAsync(int id)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Ingredient deletedIngredient = _ingredientService.Delete(id, userInfo.Id);
            return Ok(deletedIngredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
