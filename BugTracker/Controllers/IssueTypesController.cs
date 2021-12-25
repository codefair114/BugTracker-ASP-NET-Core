using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class IssueTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IssueTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.IssueTypes.ToListAsync());
        }

        // GET: IssueTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueType == null)
            {
                return NotFound();
            }

            return View(issueType);
        }

        // GET: IssueTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IssueTypeName")] IssueType issueType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issueType);
        }

        // GET: IssueTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueTypes.FindAsync(id);
            if (issueType == null)
            {
                return NotFound();
            }
            return View(issueType);
        }

        // POST: IssueTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IssueTypeName")] IssueType issueType)
        {
            if (id != issueType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueTypeExists(issueType.Id))
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
            return View(issueType);
        }

        // GET: IssueTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueType = await _context.IssueTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueType == null)
            {
                return NotFound();
            }

            return View(issueType);
        }

        // POST: IssueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueType = await _context.IssueTypes.FindAsync(id);
            _context.IssueTypes.Remove(issueType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueTypeExists(int id)
        {
            return _context.IssueTypes.Any(e => e.Id == id);
        }
    }
}
