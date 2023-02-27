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
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecipeBookService _recipeBookService;

        public IngredientsController(ApplicationDbContext context, IRecipeBookService recipeBookService)
        {
            _context = context;
            _recipeBookService = recipeBookService;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
            IEnumerable<Ingredient> model = await _recipeBookService.GetIngredientsAsync();

            return View(model); 


        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ingredient ingredient = await _recipeBookService.GetIngredientByIdAsync(id.Value);

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Measure")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _recipeBookService.AddIngredientAsync(ingredient);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            Ingredient ingredient = await _recipeBookService.GetIngredientByIdAsync(id.Value);

            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Measure")] Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _recipeBookService.UpdateIngredientAsync(ingredient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await IngredientExists(ingredient.Id))
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
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            Ingredient ingredient = await _recipeBookService.GetIngredientByIdAsync(id.Value); 

            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _recipeBookService.GetIngredientByIdAsync(id);
           
            if (ingredient != null)
            {
                await _recipeBookService.DeleteIngredientAsync(ingredient); 
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> IngredientExists(int id)
        {
          return (await _recipeBookService.GetIngredientsAsync()).Any(i => i.Id == id); 
        }
    }
}
