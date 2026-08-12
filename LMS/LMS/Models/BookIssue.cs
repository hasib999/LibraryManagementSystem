using System.ComponentModel.DataAnnotations;
namespace LMS.Models;
public class BookIssue
{
    public int Id { get; set; }
    [Display(Name = "Book")]
    public int BookId { get; set; }
    [Display(Name = "Member")]
    public int MemberId { get; set; }
    [DataType(DataType.Date)]
    public DateTime IssueDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }
    [Required, StringLength(20)]
    public string Status { get; set; } = "Issued";
    public Book Book { get; set; } = null!;
    public Member Member { get; set; } = null!;
    public BookReturn? BookReturn { get; set; }
}
