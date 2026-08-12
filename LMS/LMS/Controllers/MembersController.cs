using LMS.Data; using LMS.Models; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
namespace LMS.Controllers;
public class MembersController(ApplicationDbContext db):Controller
{
 public async Task<IActionResult> Index(string? search){var q=db.Members.AsNoTracking();if(!string.IsNullOrWhiteSpace(search)){search=search.Trim();q=q.Where(x=>x.MemberCode.Contains(search)||x.Name.Contains(search)||x.Department.Contains(search)||x.Email.Contains(search));}ViewBag.Search=search;return View(await q.OrderBy(x=>x.MemberCode).ToListAsync());}
 public async Task<IActionResult> Details(int id){var x=await db.Members.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);return x is null?NotFound():View(x);}
 public IActionResult Create()=>View(new Member());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(Member x){if(await db.Members.AnyAsync(m=>m.MemberCode==x.MemberCode))ModelState.AddModelError(nameof(x.MemberCode),"Member code already exists.");if(!ModelState.IsValid)return View(x);db.Add(x);await db.SaveChangesAsync();TempData["Success"]="Member added successfully.";return RedirectToAction(nameof(Index));}
 public async Task<IActionResult> Edit(int id){var x=await db.Members.FindAsync(id);return x is null?NotFound():View(x);}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,Member x){if(id!=x.Id)return NotFound();if(await db.Members.AnyAsync(m=>m.MemberCode==x.MemberCode&&m.Id!=id))ModelState.AddModelError(nameof(x.MemberCode),"Member code already exists.");if(!ModelState.IsValid)return View(x);db.Update(x);await db.SaveChangesAsync();TempData["Success"]="Member updated successfully.";return RedirectToAction(nameof(Index));}
 public async Task<IActionResult> Delete(int id){var x=await db.Members.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);return x is null?NotFound():View(x);}
 [HttpPost,ActionName("Delete"),ValidateAntiForgeryToken]public async Task<IActionResult> DeleteConfirmed(int id){var x=await db.Members.FindAsync(id);if(x is null)return NotFound();if(await db.BookIssues.AnyAsync(i=>i.MemberId==id)){x.IsActive=false;TempData["Success"]="Member deactivated because borrowing history exists.";}else{db.Remove(x);TempData["Success"]="Member deleted successfully.";}await db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
}
