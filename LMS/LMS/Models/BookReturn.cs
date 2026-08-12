using System.ComponentModel.DataAnnotations;
namespace LMS.Models;
public class BookReturn
{
    public int Id { get; set; }
    public int BookIssueId { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReturnDate { get; set; }
    [Range(0, int.MaxValue)]
    public int LateDays { get; set; }
    [Range(0, double.MaxValue)]
    public decimal FineAmount { get; set; }
    public BookIssue BookIssue { get; set; } = null!;
}
