using DomainLayer.Models;
using OA.Data;
using RecipeApi;
using System.Collections.Generic;  
  
namespace OA.Service
    {
        public interface IRecipeservice
        {
         IEnumerable<Recipe> GetRecipe();
        Recipe GetRecipe(long id);
        void InsertRecipe(Recipe R);
        void UpdateRecipe(Recipe R);
        void DeleteRecipe(long id);


    }



    }

