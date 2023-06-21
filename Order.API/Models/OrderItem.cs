using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [Column(TypeName = "decimal(18,2)")] //decimal değeri toplam 18 karakter 16sı virgülden önce 2si sonra
    public decimal Price { get; set; }
    public int Count { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}