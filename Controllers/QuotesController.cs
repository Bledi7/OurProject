using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurProject.Data;
using OurProject.Models;
using OurProject.ViewModels;

namespace OurProject.Controllers
{
    public class QuotesController : Controller
    {
        private readonly AppDbContext _context;
        public QuotesController(AppDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Quotes.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "CreateRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Quotes quote)
        {
            if (ModelState.IsValid)
            {
                Quotes insertQuote = new Quotes {
                  Author=quote.Author,
                  Qoute=quote.Qoute

                  };

                _context.Add(insertQuote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }




            Quotes model = new Quotes()
            {
                Id = quote.Id,
                Author = quote.Author,
                Qoute=quote.Qoute

            };
            return View(model);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Quotes quote)
        {
            if (id != quote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    var databaseArticle = _context.Quotes.Where(x => x.Id.Equals(quote.Id)).FirstOrDefault();

                    databaseArticle.Author = quote.Author;
                    databaseArticle.Qoute = quote.Qoute;


                    _context.Update(databaseArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteExists(quote.Id))
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
            return View(quote);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote = await _context.Quotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "DeleteRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quote =await  _context.Quotes.FindAsync(id);
                _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
