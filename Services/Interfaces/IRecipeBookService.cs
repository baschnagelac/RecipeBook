using RecipeBook.Models;

namespace RecipeBook.Services.Interfaces
{
    public interface IRecipeBookService
    {
        //CRUD for Recipe Books

        public Task AddRecipeBookAsync(RecipeBookM recipeBook);
        public Task<RecipeBookM> GetRecipeBookByIdAsync(int id);

        public Task UpdateRecipeBookAsync(RecipeBookM recipeBook);

        public Task DeleteRecipeBookAsync(RecipeBookM recipeBook);

        public Task<IEnumerable<RecipeBookM>>GetRecipeBooksAsync();

        public Task<IEnumerable<Recipe>>GetRecipesAsync();

        public Task<IEnumerable<Ingredient>>GetIngredientsAsync();



        //CRUD for Recipes 
        public Task AddRecipeAsync(Recipe recipe);
        public Task<Recipe> GetRecipeByIdAsync(int id);

        public Task UpdateRecipeAsync(Recipe recipeBook);

        public Task DeleteRecipeAsync(Recipe recipe);


        //CRUD for Ingredients 

        public Task AddIngredientAsync(Ingredient ingredient);
        public Task<Ingredient> GetIngredientByIdAsync(int id);

        public Task UpdateIngredientAsync(Ingredient ingredient);

        public Task DeleteIngredientAsync(Ingredient ingredient);
    }
}
