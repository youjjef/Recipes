using DomainLayer.Models;
using OA.Data;
using OA.Repo;
using RecipeApi;
using System.Collections.Generic;

namespace OA.Service
{
    public class RecipeService : IRecipeservice
    {
        private IRepository<Recipe> Reciperepository;
        //private IRepository<UserProfile> userProfileRepository;

        public RecipeService(IRepository<Recipe> Reciperepository)
        {
            this.Reciperepository = Reciperepository;
            
        }

        public IEnumerable<Recipe> GetRecipe()
        {
            return Reciperepository.GetAll();
        }

        public Recipe GetRecipe(long id)
        {
            return Reciperepository.Get(id);
        }

        public void InsertRecipe(Recipe R)
        {
            Reciperepository.Insert(R);
        }
        public void UpdateRecipe(Recipe R)
        {
            Reciperepository.Update(R);
        }

        public void DeleteRecipe(long id)
        {
            
            Recipe R = GetRecipe(id);
            Reciperepository.Remove(R);
            Reciperepository.SaveChanges();
        }
    }
}
