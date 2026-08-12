using LMS.Data;
using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LMS.Controllers;
public class ReturnsController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await db.BookIssues.AsNoTracking()
                        .Include(x => x.Book)
                        .Include(x => x.Member)
                        .Where(x => x.Status == "Issued")
                        .OrderBy(x => x.DueDate)
                        .ToListAsync());
    }
    public async Task<IActionResult> Create(int id)
    {
        var issue = await db.BookIssues.AsNoTracking()
                        .Include(x => x.Book)
                        .Include(x => x.Member)
                        .FirstOrDefaultAsync(x => x.Id == id && x.Status == "Issued");
        if (issue is null)
            return NotFound();
        ViewBag.Issue = issue;
        return View(new ReturnBookViewModel { BookIssueId = id });
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReturnBookViewModel vm)
    {
        var issue = await db.BookIssues.Include(x => x.Book)
                        .Include(x => x.Member)
                        .FirstOrDefaultAsync(x => x.Id == vm.BookIssueId);
        if (issue is null || issue.Status != "Issued" ||
            await db.BookReturns.AnyAsync(x => x.BookIssueId == vm.BookIssueId))
        {
            TempData["Error"] = "This issue has already been returned or does not exist.";
            return RedirectToAction(nameof(Index));
        }
        if (vm.ReturnDate.Date < issue.IssueDate.Date)
        {
            ModelState.AddModelError(nameof(vm.ReturnDate), "Return date cannot precede issue date.");
            ViewBag.Issue = issue;
            return View(vm);
        }
        var late = Math.Max(0, (vm.ReturnDate.Date - issue.DueDate.Date).Days);
        await using var transaction = await db.Database.BeginTransactionAsync();
        db.BookReturns.Add(new() { BookIssueId = issue.Id, ReturnDate = vm.ReturnDate.Date, LateDays = late,
                                   FineAmount = late * 10m });
        issue.Status = "Returned";
        issue.Book.AvailableQuantity++;
        await db.SaveChangesAsync();
        await transaction.CommitAsync();
        TempData["Success"] = "Book returned successfully.";
        return RedirectToAction(nameof(Index));
    }
}
