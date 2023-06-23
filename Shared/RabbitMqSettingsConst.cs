namespace Shared;

public class RabbitMqSettingsConst
{
    public const string StockReserveQueueName = "stock-reserve-queue";
    public const string StockOrderCreatedEventQueueName = "stock-order-created-queue";
    public const string StockPaymentFailedEventQueueName = "stock-payment-failed-queue";
    public const string OrderPaymentCompletedEventQueueName = "order-payment-completed-queue";
    public const string OrderPaymentFailedEventQueueName = "order-payment-failed-queue";
    public const string OrderStockNotReservedEventQueueName = "order-stock-not-reserved-queue";


    public const string OrderSaga = "order-saga-queue";

}