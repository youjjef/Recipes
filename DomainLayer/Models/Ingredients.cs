using OA.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApi
{
    public class Ingredients : BaseEntity
    {
       
        public String name { get; set; } = string.Empty;

        [ForeignKey("Recipe_ID")]
        public Recipe Recipe { get; set; }
        public int Recipe_ID { get; set; }
    }
}
