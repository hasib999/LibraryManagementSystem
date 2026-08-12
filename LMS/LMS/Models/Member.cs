using System.ComponentModel.DataAnnotations;

namespace LMS.Models;
public class Member
{
    public int Id { get; set; }
    [Required, StringLength(20), Display(Name="Member code")] public string MemberCode { get; set; } = string.Empty;
    [Required, StringLength(120)] public string Name { get; set; } = string.Empty;
    [Required, StringLength(120)] public string Department { get; set; } = string.Empty;
    [Required, Phone, StringLength(30)] public string Phone { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = string.Empty;
    [DataType(DataType.Date), Display(Name="Join date")] public DateTime JoinDate { get; set; } = DateTime.Today;
    [Display(Name="Active")] public bool IsActive { get; set; } = true;
    public ICollection<BookIssue> BookIssues { get; set; } = [];
}
