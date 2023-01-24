
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


    [HttpPost]
    [Authorize]

    public ActionResult<Ingredient> Create([FromBody] Ingredient ingredientData)
    {
        try
        {
            // Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Ingredient ingredient = _ingredientService.Create(ingredientData);
            return Ok(ingredient);


        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }


    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<string>> Remove(int id)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _ingredientService.Remove(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);
        }
    }





}
