using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using OurProject.Data;
using OurProject.Migrations;
using OurProject.Models;
using OurProject.ViewModels;

namespace OurProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pg=1)
        {
            List<Author> auhtors = _context.Author.ToList();

            const int pageSize = 3;

            if (pg < 1) pg = 1;

            int rescCunt = auhtors.Count();
            var pager = new Pager(rescCunt, pg, pageSize);
            var recSkip = (pg - 1) * pageSize;
            var data = auhtors.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                byte[] imagebytes = null;

                if (author.Photo.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await author.Photo.CopyToAsync(stream);
                        imagebytes = stream.ToArray();
                    }
                }
                else
                {
                    using (var stream = new MemoryStream())
                    {
                        await author.Photo.CopyToAsync(stream);
                        imagebytes = stream.ToArray();
                    }

                }



                Author insertAuthor = new Author
                {

                    Name = author.Name,
                    Biography = author.Biography,
                    Photo = imagebytes
                   

                };


                _context.Add(insertAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }




            CreateAuthorViewModel model = new CreateAuthorViewModel
            {
                Id=author.AuthorId,
                Name=author.Name,
                Biography=author.Biography,
                


            };
            return View(model);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAuthorViewModel author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] imagebytes = null;

                    if (author.Photo.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await author.Photo.CopyToAsync(stream);
                            imagebytes = stream.ToArray();
                        }
                    }

                    var databaseArticle = _context.Author.Where(x => x.AuthorId.Equals(author.Id)).FirstOrDefault();

                    databaseArticle.Name = author.Name;
                    databaseArticle.Biography = author.Biography;
                
                    databaseArticle.Photo = imagebytes;




                    _context.Update(databaseArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Author
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
    }
}
