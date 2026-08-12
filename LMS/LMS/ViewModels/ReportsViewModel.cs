using LMS.Models;
namespace LMS.ViewModels;
public class ReportsViewModel
{
    public List<BookIssue> CurrentlyIssued { get; set; } = [];
    public List<BookIssue> Overdue { get; set; } = [];
    public List<BookIssue> Returned { get; set; } = [];
    public decimal FineCollection { get; set; }
    public List<CategoryReportRow> BooksByCategory { get; set; } = [];
    public List<MemberReportRow> MemberHistory { get; set; } = [];
}
public record CategoryReportRow(string Category, int Titles, int Copies);
public record MemberReportRow(string MemberCode, string MemberName, int Borrowed, int Returned);
