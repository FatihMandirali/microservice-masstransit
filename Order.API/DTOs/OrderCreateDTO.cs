namespace Order.API.DTOs;

public class OrderCreateDTO
{
    public string BuyerId { get; set; }
    public PaymentDTO Payment { get; set; }
    public AddressDTO Address { get; set; }
    public List<OrderItemDTO> OrderItem { get; set; } = new List<OrderItemDTO>();
}

public class PaymentDTO
{
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public decimal TotalPrice { get; set; }
}
public class OrderItemDTO
{
    public int ProductId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}

public class AddressDTO
{
    public string Line { get; set; }
    public string Province { get; set; }
    public string District { get; set; }
}