using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeApi;


namespace RecipeAPI.Data
{
    public class DataContext : IdentityDbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Recipe> Recipes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Ingredients> Ingredientss { get; set; }

    }
}