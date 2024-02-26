using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BornToMove.DAL;

namespace BornToMove.ASPNET.Controllers
{
    public class MoveRatingsController : Controller
    {
        private readonly MoveContext _context;

        public MoveRatingsController(MoveContext context)
        {
            _context = context;
        }

        // GET: MoveRatings
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoveRating.ToListAsync());
        }

        // GET: MoveRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moveRating = await _context.MoveRating
                .FirstOrDefaultAsync(m => m.id == id);
            if (moveRating == null)
            {
                return NotFound();
            }

            return View(moveRating);
        }

        // GET: MoveRatings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoveRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Rating,Vote")] MoveRating moveRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moveRating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moveRating);
        }

        // GET: MoveRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moveRating = await _context.MoveRating.FindAsync(id);
            if (moveRating == null)
            {
                return NotFound();
            }
            return View(moveRating);
        }

        // POST: MoveRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Rating,Vote")] MoveRating moveRating)
        {
            if (id != moveRating.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moveRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoveRatingExists(moveRating.id))
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
            return View(moveRating);
        }

        // GET: MoveRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moveRating = await _context.MoveRating
                .FirstOrDefaultAsync(m => m.id == id);
            if (moveRating == null)
            {
                return NotFound();
            }

            return View(moveRating);
        }

        // POST: MoveRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moveRating = await _context.MoveRating.FindAsync(id);
            if (moveRating != null)
            {
                _context.MoveRating.Remove(moveRating);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoveRatingExists(int id)
        {
            return _context.MoveRating.Any(e => e.id == id);
        }
    }
}
