using LMS.Data;using LMS.ViewModels;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;
namespace LMS.Controllers;
public class ReportsController(ApplicationDbContext db):Controller
{
 public async Task<IActionResult> Index(){var today=DateTime.Today;var issues=db.BookIssues.AsNoTracking().Include(x=>x.Book).Include(x=>x.Member).Include(x=>x.BookReturn);return View(new ReportsViewModel{CurrentlyIssued=await issues.Where(x=>x.Status=="Issued").ToListAsync(),Overdue=await issues.Where(x=>x.Status=="Issued"&&x.DueDate<today).ToListAsync(),Returned=await issues.Where(x=>x.Status=="Returned").ToListAsync(),FineCollection=await db.BookReturns.SumAsync(x=>(decimal?)x.FineAmount)??0,BooksByCategory=await db.Books.AsNoTracking().GroupBy(x=>x.Category).Select(x=>new CategoryReportRow(x.Key,x.Count(),x.Sum(b=>b.Quantity))).ToListAsync(),MemberHistory=await db.Members.AsNoTracking().Select(x=>new MemberReportRow(x.MemberCode,x.Name,x.BookIssues.Count,x.BookIssues.Count(i=>i.Status=="Returned"))).ToListAsync()});}
}
