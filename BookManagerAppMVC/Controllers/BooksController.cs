using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagerAppMVC.Data;
using BookManagerAppMVC.Models;
using Microsoft.IdentityModel.Tokens;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Authorization;

namespace BookManagerAppMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookManagerAppMVCContext _context;

        public BooksController(BookManagerAppMVCContext context)
        {
            _context = context;
        }

        // GET: Books
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }

        // GET: Books/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                if (TempData["BookId"] != null) id = (int?)TempData["BookId"];
                else return NotFound();
            }

            var book = await _context.Book.FirstOrDefaultAsync(m => m.BookId == id);
            var rentalHistories = _context.RentalHistory.Where(x => x.BookId == id);
            ViewBag.RentalHistories = rentalHistories;
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Publisher")] Book book)
        {
            if (ModelState.IsValid)
            {
                string userName = HttpContext.User.Identity!.Name!;

                book.RegistDate = DateTime.Now;
                book.RegistUser = userName;
                book.Possetion = "本棚";
                book.UpdateUser = "";

                _context.Add(book);
                await _context.SaveChangesAsync();
                TempData["Book"] = book.BookId;
                return RedirectToAction("CreateByBooksController", "RentalHistories");
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
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
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Publisher,RegistDate,RegistUser")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string userName = HttpContext.User.Identity!.Name!;
                    book.UpdateDate = DateTime.Now;
                    book.UpdateUser = userName;
                    // 暫定処理
                    book.Possetion = "本棚";
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(FindBooks));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FindBooks));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }

        [Authorize]
        public async Task<IActionResult> FindBooks()
        {
            var books = await _context.Book.ToListAsync(); 
            return View(books);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FindBooks(string title, string author, string publisher)
        {
            var books = new List<Book>();
            if (title.IsNullOrEmpty() && author.IsNullOrEmpty() && publisher.IsNullOrEmpty())
            {
                books = await _context.Book.ToListAsync();
                return Json(books);
            }
            books = await _context.Book.Where(m => (m.Title!.Contains(title!) || title.IsNullOrEmpty())
                                                    && (m.Author!.Contains(author!) || author.IsNullOrEmpty())
                                                    && (m.Publisher!.Contains(publisher!) || publisher.IsNullOrEmpty())).ToListAsync();
            return Json(books);
        }

        public async Task<ActionResult> ShowModal(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(m => m.BookId == id);
            return PartialView("Delete", book);
        }

        /// <summary>
        /// 図書を借りるときの処理
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<IActionResult> RentBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (!book.Possetion.Equals("本棚"))
            {
                return RedirectToAction(nameof(Details));
            }
            string rentUser = HttpContext.User.Identity!.Name!;
            book.Possetion = rentUser;
            _context.Update(book);
            await _context.SaveChangesAsync();
            TempData["BookId"] = id;
            TempData["Possetion"] = rentUser;
            return RedirectToAction("RentByBooksController", "RentalHistories");
        }

        /// <summary>
        /// 図書を返却するときの処理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ReturnBook(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book.Possetion.Equals("本棚"))
            {
                return RedirectToAction(nameof(Details));
            }
            book.Possetion = "本棚";
            _context.Update(book);
            await _context.SaveChangesAsync();
            TempData["BookId"] = id;
            TempData["Possetion"] = "本棚";
            return RedirectToAction("RentByBooksController", "RentalHistories");
        }
        // GET: Books/Return
        [Authorize]
        public async Task<IActionResult> Return()
        {
            var userName = HttpContext.User.Identity!.Name!;
            var rentBooks = _context.RentalHistory.Where(x => x.Possetion == userName).ToList();
            ViewBag.RentDate = rentBooks[0].RentDate.ToString("yyyy/MM/dd");
            var book = await _context.Book.FirstOrDefaultAsync(m => m.BookId == rentBooks[0].BookId);
            return View(book);

            //// 借りている本が2冊以上ある場合(エラー)
            //if (rentBooks.Count > 1)
            //{
            //    var book = await _context.Book.FirstOrDefaultAsync(m => m.BookId == rentBooks[0].BookId);
            //    return View(book);
            //}
            //// 借りている本が1冊ある場合
            //else if (rentBooks.Count == 1)
            //{
            //    var book = await _context.Book.FirstOrDefaultAsync(m => m.BookId == rentBooks[0].BookId);
            //    return View(book);
            //}
            //// 借りている本がない場合
            //else
            //{
            //    return View();
            //}
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
