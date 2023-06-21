using MassTransit;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers;

public class StockNotReservedEventConsumer:IConsumer<StockNotReservedEvent>
{
    private readonly ILogger<StockNotReservedEventConsumer> _logger;
    private readonly AppDbContext _appDbContext;

    public StockNotReservedEventConsumer(ILogger<StockNotReservedEventConsumer> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        var orders = await _appDbContext.Orders.FindAsync(context.Message.OrderId);
        if (orders is not null)
        {
            orders.Status = OrderStatus.Fail;
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation($"{context.Message.OrderId} güncelleme başarılı stock yok ");
        }
        else
        {
            _logger.LogInformation($"{context.Message.OrderId} güncelleme fail stock yok ");

        }
    }
}