using System.ComponentModel.DataAnnotations.Schema;

namespace FreeCodeCamp.MVC.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int? SerialNumberId { get; set; }
    public SerialNumber? SerialNumber { get; set; }
    public int? CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
    public ICollection<ItemClient>? ItemClients = [];
}
