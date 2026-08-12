using LMS.Models;
namespace LMS.ViewModels;
public class DashboardViewModel
{
    public int TotalBooks { get; set; }
    public int AvailableBooks { get; set; }
    public int IssuedBooks { get; set; }
    public int TotalMembers { get; set; }
    public int OverdueBooks { get; set; }
    public decimal TotalFine { get; set; }
    public List<BookIssue> RecentIssues { get; set; } = [];
}
