using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers;

public class PaymentSuccessedEventConsumer:IConsumer<PaymentSuccessedEvent>
{
    private readonly ILogger<PaymentSuccessedEventConsumer> _logger;
    private readonly AppDbContext _appDbContext;

    public PaymentSuccessedEventConsumer(ILogger<PaymentSuccessedEventConsumer> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public async Task Consume(ConsumeContext<PaymentSuccessedEvent> context)
    {
        var orders = await _appDbContext.Orders.FindAsync(context.Message.OrderId);
        if (orders is not null)
        {
            orders.Status = OrderStatus.Success;
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation($"{context.Message.OrderId} güncelleme başarılı ");
        }
        else
        {
            _logger.LogInformation($"{context.Message.OrderId} güncelleme fail ");

        }
    }
}