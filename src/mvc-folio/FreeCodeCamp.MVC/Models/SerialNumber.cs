using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCodeCamp.MVC.Models;

public class SerialNumber
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ItemId { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Item? Item { get; set; }
}
