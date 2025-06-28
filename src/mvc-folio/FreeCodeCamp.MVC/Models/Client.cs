namespace FreeCodeCamp.MVC.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<ItemClient>? ItemClients = [];
}
