using System.ComponentModel.DataAnnotations;

namespace LMS.Models;
public class Book
{
    public int Id { get; set; }
    [Required, StringLength(20)]
    public string ISBN { get; set; } = string.Empty;
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Author { get; set; } = string.Empty;
    [Required, StringLength(80)]
    public string Category { get; set; } = string.Empty;
    [StringLength(100)]
    public string? Publisher { get; set; }
    [Range(1000, 2100), Display(Name = "Publication year")]
    public int PublicationYear { get; set; }
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    [Range(0, int.MaxValue), Display(Name = "Available quantity")]
    public int AvailableQuantity { get; set; }
    [Required, StringLength(20), Display(Name = "Shelf number")]
    public string ShelfNumber { get; set; } = string.Empty;
    public ICollection<BookIssue> BookIssues { get; set; } = [];
}
