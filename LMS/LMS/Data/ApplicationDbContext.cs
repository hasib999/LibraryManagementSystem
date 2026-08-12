using LMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<BookIssue> BookIssues => Set<BookIssue>();
    public DbSet<BookReturn> BookReturns => Set<BookReturn>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Book>().HasIndex(x => x.ISBN).IsUnique();
        builder.Entity<Member>().HasIndex(x => x.MemberCode).IsUnique();
        builder.Entity<BookIssue>()
            .HasOne(x => x.Book)
            .WithMany(x => x.BookIssues)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<BookIssue>()
            .HasOne(x => x.Member)
            .WithMany(x => x.BookIssues)
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<BookReturn>().HasIndex(x => x.BookIssueId).IsUnique();
        builder.Entity<BookReturn>()
            .HasOne(x => x.BookIssue)
            .WithOne(x => x.BookReturn)
            .HasForeignKey<BookReturn>(x => x.BookIssueId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<BookReturn>().Property(x => x.FineAmount).HasPrecision(10, 2);
    }
}
