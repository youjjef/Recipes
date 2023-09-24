using DomainLayer;
using OA.Data;

namespace RecipeApi
{
    public class Recipe: BaseEntity

    {
       
        public String Recipe_name { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public List<Ingredients> ingredients { get; set; }  
        public string steps { get; set; } = string.Empty;

    }
}