using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data;
public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();
        var roles = services.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in new[] { "Admin", "Librarian" }) if (!await roles.RoleExistsAsync(role)) await roles.CreateAsync(new(role));
        var users = services.GetRequiredService<UserManager<ApplicationUser>>();
        await CreateUser(users, "admin@library.com", "Admin@123", "System Administrator", "Admin");
        await CreateUser(users, "librarian@library.com", "Library@123", "Library Officer", "Librarian");

        if (!await db.Books.AnyAsync())
        {
            db.Books.AddRange(
                B("9780132350884","Clean Code","Robert C. Martin","Programming","Prentice Hall",2008,5,"A-01"),
                B("9780131103627","The C Programming Language","Brian W. Kernighan","Programming","Prentice Hall",1988,4,"A-02"),
                B("9780262046305","Introduction to Algorithms","Thomas H. Cormen","Algorithms","MIT Press",2022,4,"A-03"),
                B("9780073523323","Database System Concepts","Abraham Silberschatz","Database","McGraw-Hill",2019,5,"B-01"),
                B("9780132126953","Computer Networks","Andrew S. Tanenbaum","Networking","Pearson",2010,3,"B-02"),
                B("9781119800361","Operating System Concepts","Abraham Silberschatz","Operating System","Wiley",2021,4,"B-03"),
                B("9780134494166","Clean Architecture","Robert C. Martin","Software Engineering","Pearson",2017,3,"C-01"),
                B("9780134685991","Effective Java","Joshua Bloch","Programming","Addison-Wesley",2018,4,"C-02"),
                B("9781491918661","Learning PHP, MySQL & JavaScript","Robin Nixon","Web Development","O'Reilly",2018,3,"C-03"),
                B("9780133943030","Software Engineering","Ian Sommerville","Software Engineering","Pearson",2015,5,"C-04"));
            await db.SaveChangesAsync();
        }
        if (!await db.Members.AnyAsync())
        {
            var names = new[] { "Md. Bappi", "Mst. Shakila Akther Munmun", "Nushrat Islam Nisha", "Ripon Chandra Pal", "Md. Saifur Islam", "Rahim Ahmed", "Karim Hasan", "Nusrat Jahan" };
            var deps = new[] { "Computer Science & Engineering", "Electrical & Electronic Engineering", "Business Administration" };
            for (var i=0;i<names.Length;i++) db.Members.Add(new() { MemberCode=$"MEM-{i+1:0000}", Name=names[i], Department=deps[i%3], Phone=$"0171000000{i+1}", Email=$"member{i+1}@university.edu", JoinDate=DateTime.Today.AddMonths(-i-2), IsActive=true });
            await db.SaveChangesAsync();
        }
        if (!await db.BookIssues.AnyAsync())
        {
            var books=await db.Books.OrderBy(x=>x.Id).Take(5).ToListAsync(); var members=await db.Members.OrderBy(x=>x.Id).Take(5).ToListAsync(); var today=DateTime.Today;
            var issues = new[] {
                new BookIssue { Book=books[0], Member=members[0], IssueDate=today.AddDays(-12), DueDate=today.AddDays(-5), Status="Issued" },
                new BookIssue { Book=books[1], Member=members[1], IssueDate=today.AddDays(-3), DueDate=today.AddDays(4), Status="Issued" },
                new BookIssue { Book=books[2], Member=members[2], IssueDate=today.AddDays(-2), DueDate=today.AddDays(5), Status="Issued" },
                new BookIssue { Book=books[3], Member=members[3], IssueDate=today.AddDays(-20), DueDate=today.AddDays(-13), Status="Returned" },
                new BookIssue { Book=books[4], Member=members[4], IssueDate=today.AddDays(-10), DueDate=today.AddDays(-3), Status="Returned" }};
            db.BookIssues.AddRange(issues); foreach(var i in issues.Where(x=>x.Status=="Issued")) i.Book.AvailableQuantity--;
            db.BookReturns.AddRange(new BookReturn { BookIssue=issues[3], ReturnDate=today.AddDays(-10), LateDays=3, FineAmount=30 }, new BookReturn { BookIssue=issues[4], ReturnDate=today.AddDays(-4), LateDays=0, FineAmount=0 });
            await db.SaveChangesAsync();
        }
    }
    private static Book B(string isbn,string title,string author,string category,string publisher,int year,int quantity,string shelf) => new() { ISBN=isbn,Title=title,Author=author,Category=category,Publisher=publisher,PublicationYear=year,Quantity=quantity,AvailableQuantity=quantity,ShelfNumber=shelf };
    private static async Task CreateUser(UserManager<ApplicationUser> manager,string email,string password,string name,string role)
    { var user=await manager.FindByEmailAsync(email); if(user is null) { user=new(){UserName=email,Email=email,EmailConfirmed=true,FullName=name}; var result=await manager.CreateAsync(user,password); if(!result.Succeeded) throw new InvalidOperationException(string.Join("; ",result.Errors.Select(x=>x.Description))); } if(!await manager.IsInRoleAsync(user,role)) await manager.AddToRoleAsync(user,role); }
}
