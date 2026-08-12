using System.Diagnostics;
using LMS.Data;
using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LMS.Controllers;
public class HomeController(ApplicationDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;
        return View(new DashboardViewModel { TotalBooks=await db.Books.CountAsync(), AvailableBooks=await db.Books.SumAsync(x=>x.AvailableQuantity), IssuedBooks=await db.BookIssues.CountAsync(x=>x.Status=="Issued"), TotalMembers=await db.Members.CountAsync(x=>x.IsActive), OverdueBooks=await db.BookIssues.CountAsync(x=>x.Status=="Issued"&&x.DueDate<today), TotalFine=await db.BookReturns.SumAsync(x=>(decimal?)x.FineAmount)??0, RecentIssues=await db.BookIssues.AsNoTracking().Include(x=>x.Book).Include(x=>x.Member).OrderByDescending(x=>x.IssueDate).Take(5).ToListAsync() });
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ??
                                                                          HttpContext.TraceIdentifier });
}
