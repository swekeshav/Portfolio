using System.ComponentModel.DataAnnotations;

namespace Portfolio.Web.Models;

public class TodoInputViewModel
{
    [Required]
    [Display (Name ="Title")]
    public string? Title { get; set; }
}
