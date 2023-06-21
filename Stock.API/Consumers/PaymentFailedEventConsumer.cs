using MassTransit;
using Shared;
using Stock.API.Models;

namespace Stock.API.Consumers;

public class PaymentFailedEventConsumer:IConsumer<PaymentFailedEvent>
{
    private readonly ILogger<PaymentFailedEventConsumer> _logger;
    private readonly AppDbContext _appDbContext;

    public PaymentFailedEventConsumer(ILogger<PaymentFailedEventConsumer> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        //ödeme alınamadığı için stock'u geri ver
        throw new NotImplementedException();
    }
}