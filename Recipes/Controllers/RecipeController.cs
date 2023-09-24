using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using RecipeApi;
using RecipeAPI.Data;
using Microsoft.IdentityModel.Tokens;
using OA.Service;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeservice recipeservice;

        // private readonly object DataContext;

        public RecipeController(IRecipeservice recipeservice)
        {
            this.recipeservice = recipeservice;
        }



        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> Get()
        {

            return Ok( this.recipeservice.GetRecipe());

        }
        [HttpGet("{search}")]

        public async Task<ActionResult<IEnumerable<Recipe>>> search(string Name)
        {
            var query = this.recipeservice.GetRecipe();
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(a => a.Recipe_name.ToLower().Contains(Name.ToLower()));


            }
            return  query.ToList();
        }
        [HttpPost]
        public async Task<ActionResult<List<Recipe>>> Add(Recipe R)
        {
            this.recipeservice.InsertRecipe(R);
         

            return Ok(recipeservice.GetRecipe().ToList());
        }
        /**        [HttpPut]
        public async Task<ActionResult<List<Recipe>>> Edit(Recipe R)
        {
            var recipe=this.dataContext.Recipes.Find(R.Id);
            recipe = R;
            this.dataContext.Recipes.Update(recipe);
            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.Recipes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Recipe>>> Delete(int id)
        {
            var dbrecipe = await this.dataContext.Recipes.FindAsync(id);
            if (dbrecipe == null)
                return BadRequest("recipe not found.");

            this.dataContext.Recipes.Remove(dbrecipe);
            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.Recipes.ToListAsync());
        }
        //filter by ingredients

        public List<Recipe> filterIngredients(string ingredients)
        {
            ingredients = string.IsNullOrEmpty(ingredients) ? "" : ingredients.ToLower();
            var Lista=ingredients.Split(",");
            List<Recipe>filters =new List<Recipe>();
            var recipes = from R in this.dataContext.Recipes select R;
            foreach(var ingr in Lista)
            {
                recipes=recipes.Where(X => X.ingredients.ToString().ToLower().Contains(ingr));
            }
            return recipes.ToList();
        }
        //search
        public List<Recipe> filter(string nameORingredients)
        {
            nameORingredients = string.IsNullOrEmpty(nameORingredients) ? "" : nameORingredients.ToLower();
            List<Recipe> filters = new List<Recipe>();
            var recipes = (from R in this.dataContext.Recipes where nameORingredients==""||R.Recipe_name.ToLower().Contains(nameORingredients)
            select new Recipe
            {
                Id = R.Id,
                Recipe_name = R.Recipe_name,
                ingredients = R.ingredients,
                steps = R.steps,
            }
            );
            return recipes.ToList();
        }



        */
    }


}