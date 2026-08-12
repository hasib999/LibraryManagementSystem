using System.ComponentModel.DataAnnotations;
namespace LMS.ViewModels;
public class ReturnBookViewModel
{
    public int BookIssueId { get; set; }
    [DataType(DataType.Date), Display(Name="Return date")] public DateTime ReturnDate { get; set; } = DateTime.Today;
}
