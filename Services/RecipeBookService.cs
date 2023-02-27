using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models;
using RecipeBook.Services.Interfaces;
using System.Net.Sockets;

namespace RecipeBook.Services
{
    public class RecipeBookService : IRecipeBookService
    {
        private readonly ApplicationDbContext _context;

        public RecipeBookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            try
            {
                await _context.AddAsync(ingredient);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            try
            {
                await _context.AddAsync(recipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }

        public async Task AddRecipeBookAsync(RecipeBookM recipeBook)
        {
            try
            {
                await _context.AddAsync(recipeBook);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            try
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteRecipeAsync(Recipe recipe)
        {

            try
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        public async Task DeleteRecipeBookAsync(RecipeBookM recipeBook)
        {
            try
            {
                _context.RecipeBooks.Remove(recipeBook);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            try
            {
                Ingredient? ingredient = await _context.Ingredients
                                                       .Include(i => i.Recipes)
                                                            .ThenInclude(i => i.RecipeBook)
                                                        .FirstOrDefaultAsync(i => i.Id == ingredientId);

                return ingredient!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            IEnumerable<Ingredient> ingredients = await _context.Ingredients
                                                                    .ToListAsync();
            return ingredients;
        }

        public async Task<RecipeBookM>GetRecipeBookByIdAsync(int recipeBookId)
        {
            try
            {
                RecipeBookM? recipeBook = await _context.RecipeBooks
                                                        .Include(b => b.Recipes)
                                                        .FirstOrDefaultAsync(b => b.Id == recipeBookId);

                return recipeBook!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<RecipeBookM>> GetRecipeBooksAsync()
        {
            try
            {
                IEnumerable<RecipeBookM> recipeBooks = await _context.RecipeBooks
                                                                    .Include(b => b.Recipes)
                                                                    .ToListAsync();
                return recipeBooks;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            try
            {
                Recipe? recipe = await _context.Recipes
                                               .Include(r => r.Ingredients)
                                               .FirstOrDefaultAsync(recipe => recipe.Id == recipeId);

                return recipe!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            try
            {
                IEnumerable<Recipe> recipes = await _context.Recipes
                                                            .Include(b => b.Ingredients)
                                                            .ToListAsync();
                return recipes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            try
            {
                _context.Update(ingredient);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            try
            {
                _context.Update(recipe);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateRecipeBookAsync(RecipeBookM recipeBook)
        {
            _context.Update(recipeBook);

            await _context.SaveChangesAsync();
        }
    }
}
