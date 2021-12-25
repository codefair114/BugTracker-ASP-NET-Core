using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Controllers
{
    public class TempoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TempoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tempo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tempo.Include(t => t.Issue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tempo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempo = await _context.Tempo
                .Include(t => t.Issue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tempo == null)
            {
                return NotFound();
            }

            return View(tempo);
        }

        // GET: Tempo/Create
        public IActionResult Create()
        {
            TempoModel tempoCreateModel = new TempoModel();
            tempoCreateModel.Tempo = new Tempo();
            List<SelectListItem> issues = _context.Issues
                .OrderBy(n => n.Id + " - " +n.Summary)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.Id + " - " + n.Summary
                 }).ToList();

            var userId = _userManager.GetUserName(HttpContext.User);
            tempoCreateModel.Tempo.CreatedAt = DateTime.Now;
            tempoCreateModel.Tempo.ModifiedAt = DateTime.Now;

            tempoCreateModel.Tempo.AssigneeName = userId;
            tempoCreateModel.Issue = issues;
            return View(tempoCreateModel);
        }

        // POST: Tempo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Summary,Description,Hours,AssigneeName,IssueId,CreatedAt,ModifiedAt")] Tempo tempo)
        {
            if (ModelState.IsValid)
            {
                tempo.CreatedAt = DateTime.Now;
                tempo.ModifiedAt = DateTime.Now;
                _context.Add(tempo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssueId"] = new SelectList(_context.Issues, "Id", "Description", tempo.IssueId);
            return View(tempo);
        }

        // GET: Tempo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempo = await _context.Tempo.FindAsync(id);
            if (tempo == null)
            {
                return NotFound();
            }
            TempoModel tempoEditModel = new TempoModel();
            tempoEditModel.Tempo = tempo;
            List<SelectListItem> issues = _context.Issues
                .OrderBy(n => n.Id + " - " + n.Summary)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.Id + " - " + n.Summary
                 }).ToList();

            var userId = _userManager.GetUserName(HttpContext.User);
            tempoEditModel.Tempo.ModifiedAt = DateTime.Now;
            tempoEditModel.Tempo.AssigneeName = userId;
            tempoEditModel.Issue = issues;
            return View(tempoEditModel);
        }

        // POST: Tempo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Summary,Description,Hours,AssigneeName,IssueId,CreatedAt,ModifiedAt")] Tempo tempo)
        {
            if (id != tempo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tempo.ModifiedAt = DateTime.Now;
                    _context.Update(tempo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempoExists(tempo.Id))
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
            ViewData["IssueId"] = new SelectList(_context.Issues, "Id", "Description", tempo.IssueId);
            return View(tempo);
        }

        // GET: Tempo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempo = await _context.Tempo
                .Include(t => t.Issue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tempo == null)
            {
                return NotFound();
            }

            return View(tempo);
        }

        // POST: Tempo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tempo = await _context.Tempo.FindAsync(id);
            _context.Tempo.Remove(tempo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TempoExists(int id)
        {
            return _context.Tempo.Any(e => e.Id == id);
        }
    }
}
