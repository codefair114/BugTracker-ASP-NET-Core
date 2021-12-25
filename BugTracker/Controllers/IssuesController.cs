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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BugTracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Issues.Include(i => i.IssueType).Include(i => i.Priority).Include(i => i.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.IssueType)
                .Include(i => i.Priority)
                .Include(i => i.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            IssueModel issueCreateModel = new IssueModel();
            issueCreateModel.Issue = new Issue();
            List<SelectListItem> projects = _context.Projects
                .OrderBy(n => n.Name)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.Name
                 }).ToList();
            List<SelectListItem> issueTypes = _context.IssueTypes
                .OrderBy(n => n.IssueTypeName)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.IssueTypeName
                 }).ToList();

            var userId = _userManager.GetUserName(HttpContext.User);

            issueCreateModel.Issue.ReporterName = userId;
            issueCreateModel.Issue.CreatedAt = DateTime.Now;
            issueCreateModel.Issue.ModifiedAt = DateTime.Now;

            List<SelectListItem> assignees = _userManager.Users
                .OrderBy(n => n.Id)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.UserName,
                    Text = n.UserName
                }).ToList();

            List<SelectListItem> priorities = _context.PriorityTypes
                .OrderBy(n => n.PriorityName)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.PriorityName
                 }).ToList();

            issueCreateModel.Projects = projects;
            issueCreateModel.IssueType = issueTypes;
            issueCreateModel.Priority = priorities;
            issueCreateModel.Assignee = assignees;
            return View(issueCreateModel);
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,IssueTypeId,Summary,Description,ReporterName,AssigneeName,PriorityId")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                issue.CreatedAt = DateTime.Now;
                issue.ModifiedAt = DateTime.Now;
                _context.Add(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssueTypeId"] = new SelectList(_context.IssueTypes, "Id", "IssueTypeName", issue.IssueTypeId);
            ViewData["PriorityId"] = new SelectList(_context.PriorityTypes, "Id", "PriorityName", issue.PriorityId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "CreatorId", issue.ProjectId);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            IssueModel issueEditModel = new IssueModel();
            issueEditModel.Issue = issue;
            List<SelectListItem> projects = _context.Projects
                .OrderBy(n => n.Name)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.Name
                 }).ToList();
            List<SelectListItem> issueTypes = _context.IssueTypes
                .OrderBy(n => n.IssueTypeName)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.IssueTypeName
                 }).ToList();

            //var userId = _userManager.GetUserName(HttpContext.User);

            //issueEditModel.Issue.ReporterName = userId;

            issueEditModel.Issue.ModifiedAt = DateTime.Now;
            List<SelectListItem> assignees = _userManager.Users
                .OrderBy(n => n.Id)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.UserName,
                    Text = n.UserName
                }).ToList();

            List<SelectListItem> priorities = _context.PriorityTypes
                .OrderBy(n => n.PriorityName)
                .Select(n =>
                 new SelectListItem
                 {
                     Value = n.Id.ToString(),
                     Text = n.PriorityName
                 }).ToList();

            issueEditModel.Projects = projects;
            issueEditModel.IssueType = issueTypes;
            issueEditModel.Priority = priorities;
            issueEditModel.Assignee = assignees;
            return View(issueEditModel);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectId,IssueTypeId,Summary,Description,ReporterName,AssigneeName,PriorityId")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    issue.ModifiedAt = DateTime.Now;
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            ViewData["IssueTypeId"] = new SelectList(_context.IssueTypes, "Id", "IssueTypeName", issue.IssueTypeId);
            ViewData["PriorityId"] = new SelectList(_context.PriorityTypes, "Id", "PriorityName", issue.PriorityId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "CreatorId", issue.ProjectId);
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.IssueType)
                .Include(i => i.Priority)
                .Include(i => i.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }
    }
}
