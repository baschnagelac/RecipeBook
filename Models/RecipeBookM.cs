using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Models
{
    public class RecipeBookM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 2)] 
        public string? Title { get; set; }

        public string? RBUserId { get; set; }

        //navigational properties 
        //recipe book should belong to a single user (RBUser) 
        public virtual RBUser? RBUser { get; set; }

        //recipe book should have a collection of recipes 


        public int RecipeId { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
