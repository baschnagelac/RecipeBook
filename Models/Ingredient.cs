namespace RecipeBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Measure { get; set; }

        //navigational properties 
        //an ingredient will have a collection of recipes that it may belong to
        public virtual ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
