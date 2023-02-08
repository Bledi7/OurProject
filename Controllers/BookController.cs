using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurProject.Data;
using OurProject.Models;
using OurProject.ViewModels;

namespace OurProject.Controllers
{
   
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        
        public BookController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Books
       
        public IActionResult Index()
        {
            var data=_context.Book.ToList();
            return View(data);
        }


        // GET: Books/Details/5
   
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
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
        public async Task<IActionResult> Create(CreateBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                byte[] imagebytes = null;

                if (book.Image.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await book.Image.CopyToAsync(stream);
                        imagebytes = stream.ToArray();
                    }
                }
                else
                {
                    using (var stream = new MemoryStream())
                    {
                        await book.Image.CopyToAsync(stream);
                        imagebytes = stream.ToArray();
                    }

                }



                Book insertBook = new Book
                {

                    Author = book.Author,
                    Title = book.Title,
                    Category = book.Category,
                    Image = imagebytes

                };


                _context.Add(insertBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }




            CreateBookViewModel model = new CreateBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Category = book.Category,
                Author = book.Author,




            };
            return View(model);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "EditRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateBookViewModel book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] imagebytes = null;

                    if (book.Image.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await book.Image.CopyToAsync(stream);
                            imagebytes = stream.ToArray();
                        }
                    }

                    var databaseArticle = _context.Book.Where(x => x.Id.Equals(book.Id)).FirstOrDefault();

                    databaseArticle.Author = book.Author;
                    databaseArticle.Title = book.Title;
                    databaseArticle.Category = book.Category;
                    databaseArticle.Image = imagebytes;




                    _context.Update(databaseArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]

        [Authorize(Policy = "DeleteRolePolicy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}

