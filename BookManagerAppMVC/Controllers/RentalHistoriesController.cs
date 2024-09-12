using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagerAppMVC.Data;
using BookManagerAppMVC.Models;

namespace BookManagerAppMVC.Controllers
{
    public class RentalHistoriesController : Controller
    {
        private readonly BookManagerAppMVCContext _context;

        public RentalHistoriesController(BookManagerAppMVCContext context)
        {
            _context = context;
        }

        // GET: RentalHistories
        public async Task<IActionResult> Index()
        {
            return View(await _context.RentalHistory.ToListAsync());
        }

        // GET: RentalHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalHistory = await _context.RentalHistory
                .FirstOrDefaultAsync(m => m.RentalHistoryId == id);
            if (rentalHistory == null)
            {
                return NotFound();
            }

            return View(rentalHistory);
        }

        // GET: RentalHistories/Create
        public IActionResult Create()
        {
            ViewBag.BookId = new SelectList(_context.Book, "BookId", "Title");
            return View();
        }

        // POST: RentalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalHistoryId,BookId")] RentalHistory rentalHistory)
        {
            
            if (ModelState.IsValid)
            {
                string userName = HttpContext.User.Identity!.Name!;

                rentalHistory.RentDate = DateTime.Now;
                rentalHistory.RentUser = userName;
                _context.Add(rentalHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.BookId = new SelectList(_context.Book, "BookId", "Title");
            return View(rentalHistory);
        }

        /// <summary>
        /// 図書情報を作成したときの初期状態を登録する
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateByBooksController()
        {
            int bookId = (int)TempData["Book"]!;
            var rentalHistory = new RentalHistory();

            rentalHistory.RentDate = DateTime.Now;
            rentalHistory.RentUser = "本棚";
            rentalHistory.BookId = bookId;
            _context.Add(rentalHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction("FindBooks","Books");
        }

        // GET: RentalHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalHistory = await _context.RentalHistory.FindAsync(id);
            if (rentalHistory == null)
            {
                return NotFound();
            }
            return View(rentalHistory);
        }

        // POST: RentalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalHistoryId,RentDate,RentUser")] RentalHistory rentalHistory)
        {
            if (id != rentalHistory.RentalHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentalHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalHistoryExists(rentalHistory.RentalHistoryId))
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
            return View(rentalHistory);
        }

        // GET: RentalHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalHistory = await _context.RentalHistory
                .FirstOrDefaultAsync(m => m.RentalHistoryId == id);
            if (rentalHistory == null)
            {
                return NotFound();
            }

            return View(rentalHistory);
        }

        // POST: RentalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalHistory = await _context.RentalHistory.FindAsync(id);
            if (rentalHistory != null)
            {
                _context.RentalHistory.Remove(rentalHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalHistoryExists(int id)
        {
            return _context.RentalHistory.Any(e => e.RentalHistoryId == id);
        }
    }
}
