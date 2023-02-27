using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models;
using RecipeBook.Services;
using RecipeBook.Services.Interfaces;

namespace RecipeBook.Controllers
{
    public class RecipeBookMsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<RBUser> _userManager;
        private readonly IRecipeBookService _recipeBookService;

        public RecipeBookMsController(ApplicationDbContext context, UserManager<RBUser> userManager, IRecipeBookService recipeBookService)
        {
            _context = context;
            _userManager = userManager;
            _recipeBookService = recipeBookService;
        }

        // GET: RecipeBookMs
        public async Task<IActionResult> Index()
        {
            string? userId = _userManager.GetUserId(User);

            var recipeBooks = await _recipeBookService.GetRecipeBooksAsync();
            return View(recipeBooks); 
        }

        // GET: RecipeBookMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RecipeBookM recipeBook = await _recipeBookService.GetRecipeBookByIdAsync(id.Value);

            if (recipeBook == null)
            {
                return NotFound();
            }

            return View(recipeBook);
        }

        // GET: RecipeBookMs/Create
        public async Task<IActionResult> Create()
        {

            string? userId = _userManager.GetUserId(User);
            ViewData["RecipeList"] = new MultiSelectList(await _recipeBookService.GetRecipesAsync(), "Id", "Name");
            return View();
        }

        // POST: RecipeBookMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,RBUserId")] RecipeBookM recipeBook)
        {
            ModelState.Remove("RBUserId");
            
            if (ModelState.IsValid)
            {
                recipeBook.RBUserId = _userManager.GetUserId(User);

                await _recipeBookService.AddRecipeBookAsync(recipeBook);

                ViewData["RecipeList"] = new MultiSelectList(await _recipeBookService.GetRecipesAsync(), "Id", "Name");
                return RedirectToAction(nameof(Index));
            }

            return View(recipeBook);
        }

        // GET: RecipeBookMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RecipeBookM recipeBook = await _recipeBookService.GetRecipeBookByIdAsync(id.Value);

            if (recipeBook == null)
            {
                return NotFound();
            }

            string? userId = _userManager.GetUserId(User);

            ViewData["RecipeList"] = new MultiSelectList(await _recipeBookService.GetRecipesAsync(), "Id", "Name");

            return View(recipeBook);
        }

        // POST: RecipeBookMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,RBUserId")] RecipeBookM recipeBook)
        {
            if (id != recipeBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string? userId = _userManager.GetUserId(User);

                    await _recipeBookService.UpdateRecipeBookAsync(recipeBook);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await RecipeBookMExists(recipeBook.Id))
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
            ViewData["RecipeList"] = new MultiSelectList(await _recipeBookService.GetRecipesAsync(), "Id", "Name");
            return View(recipeBook);
        }

        // GET: RecipeBookMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeBook = await _recipeBookService.GetRecipeBookByIdAsync(id.Value);

            if (recipeBook == null)
            {
                return NotFound();
            }

            return View(recipeBook);
        }

        // POST: RecipeBookMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecipeBooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RecipeBooks'  is null.");
            }
            var recipeBook = await _recipeBookService.GetRecipeBookByIdAsync(id);
            
            if (recipeBook != null)
            {
                await _recipeBookService.DeleteRecipeBookAsync(recipeBook);
            }
            ;
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RecipeBookMExists(int id)
        {
            return (await _recipeBookService.GetRecipeBooksAsync()).Any(e => e.Id == id);
                
               
        }
    }
}
