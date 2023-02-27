using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models;
using RecipeBook.Services.Interfaces;

namespace RecipeBook.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly IRecipeBookService _recipeBookService;

        public RecipesController(ApplicationDbContext context, IImageService imageService, IRecipeBookService recipeBookService)
        {
            _context = context;
            _imageService = imageService;
            _recipeBookService = recipeBookService;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var recipe = await _recipeBookService.GetRecipesAsync();
            return View(recipe);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe recipe = await _recipeBookService.GetRecipeByIdAsync(id.Value);
          
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public async Task<IActionResult> Create()
        {
            

            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Created,ImageData,ImageType")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                // INSERT IMAGE SERVICE 

                if (recipe.ImageFile != null)
                {
                    recipe.ImageData = await _imageService.ConvertFileToByteArrayAsync(recipe.ImageFile);
                    recipe.ImageType = recipe.ImageFile.ContentType;
                }

                recipe.Created = DateTime.UtcNow;

                await _recipeBookService.AddRecipeAsync(recipe);

              
                return RedirectToAction(nameof(Index));

            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var recipe = await _recipeBookService.GetRecipeByIdAsync(id.Value);
            if (recipe == null)
            {
                return NotFound();
            }

            

            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Created,ImageData,ImageType")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // INSERT IMAGE SERVICE 

                    if (recipe.ImageFile != null)
                    {
                        recipe.ImageData = await _imageService.ConvertFileToByteArrayAsync(recipe.ImageFile);
                        recipe.ImageType = recipe.ImageFile.ContentType;
                    }

                    recipe.Created = DateTime.SpecifyKind(recipe.Created, DateTimeKind.Utc);

                    await _recipeBookService.UpdateRecipeAsync(recipe); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new MultiSelectList(await _recipeBookService.GetRecipesAsync(), "Id", "Name");
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _recipeBookService.GetRecipeByIdAsync(id.Value);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _recipeBookService.GetRecipeByIdAsync(id);
           
            if (recipe != null)
            {
                await _recipeBookService.DeleteRecipeAsync(recipe);
            }
            
           
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RecipeExists(int id)
        {
            return (await _recipeBookService.GetRecipesAsync()).Any(c => c.Id == id);
        }
    }
}
