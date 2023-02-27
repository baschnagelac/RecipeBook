using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RecipeBook.Models;

namespace RecipeBook.Data
{
    public class ApplicationDbContext : IdentityDbContext<RBUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RecipeBookM> RecipeBooks { get; set; } = default!;

        public virtual DbSet<Recipe> Recipes { get; set; } = default!;

        public virtual DbSet<Ingredient> Ingredients { get; set;} = default!;

    }
}