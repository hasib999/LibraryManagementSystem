using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace LMS.ViewModels;
public class IssueBookViewModel : IValidatableObject
{
    [Required, Display(Name="Member")] public int? MemberId { get; set; }
    [Required, Display(Name="Book")] public int? BookId { get; set; }
    [DataType(DataType.Date), Display(Name="Issue date")] public DateTime IssueDate { get; set; } = DateTime.Today;
    [DataType(DataType.Date), Display(Name="Due date")] public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
    public IEnumerable<SelectListItem> Members { get; set; } = [];
    public IEnumerable<SelectListItem> Books { get; set; } = [];
    public IEnumerable<ValidationResult> Validate(ValidationContext _) { if (DueDate < IssueDate) yield return new("Due date must be on or after issue date.", [nameof(DueDate)]); }
}
