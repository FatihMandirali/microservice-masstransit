using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers;

public class PaymentFailedEventConsumer:IConsumer<PaymentFailedEvent>
{
    private readonly ILogger<PaymentSuccessedEventConsumer> _logger;
    private readonly AppDbContext _appDbContext;

    public PaymentFailedEventConsumer(ILogger<PaymentSuccessedEventConsumer> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var orders = await _appDbContext.Orders.FindAsync(context.Message.OrderId);
        if (orders is not null)
        {
            orders.Status = OrderStatus.Fail;
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation($"{context.Message.OrderId} güncelleme başarılı ödeme fail ");
        }
        else
        {
            _logger.LogInformation($"{context.Message.OrderId} güncelleme fail ödeme fail ");

        }
    }
}