using System.Text;
using MassTransit;

namespace SagaStateMachineWorkerService.Model;

public class OrderStateInstance:SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public string BuyerId { get; set; }
    public int OrderId { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public override string ToString()
    {
        var properties = GetType().GetProperties();
        var sp = new StringBuilder();
        properties.ToList().ForEach(x =>
        {
            var value = x.GetValue(this, null);
            sp.Append($"{x.Name}:{value}");
        });
        sp.Append("-----");
        return sp.ToString();
    }
}