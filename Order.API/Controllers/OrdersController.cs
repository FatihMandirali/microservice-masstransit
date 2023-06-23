using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Models;
using Shared;
using Shared.Events;

namespace Order.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public OrdersController(AppDbContext context, ISendEndpointProvider sendEndpointProvider)
    {
        _context = context;
        _sendEndpointProvider = sendEndpointProvider;
    }


    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateDTO orderCreateDto)
    {
        var newOrder = new Models.Order
        {
            BuyerId = orderCreateDto.BuyerId,
            Status = OrderStatus.Suspend,
            Address = new Address
            {
                District = orderCreateDto.Address.District,
                Line = orderCreateDto.Address.Line,
                Province = orderCreateDto.Address.Province
            },
            CreatedTime = DateTime.Now,
            FailMessage = ""

        };
        orderCreateDto.OrderItem.ForEach(item =>
        {
            newOrder.Items.Add(new OrderItem
            {
                Price = item.Price,
                Count = item.Count,
                ProductId = item.ProductId
            });
        });

        await _context.AddAsync(newOrder);
        await _context.SaveChangesAsync();

        var orderCreatedEvent = new OrderCreatedRequestEvent()
        {
            BuyerId = orderCreateDto.BuyerId,
            OrderId = newOrder.Id,
            Payment = new PaymentMessage
            {
                CardName = orderCreateDto.Payment.CardName,
                CVV = orderCreateDto.Payment.CVV,
                CardNumber = orderCreateDto.Payment.CardNumber,
                Expiration = orderCreateDto.Payment.Expiration,
                TotalPrice = orderCreateDto.OrderItem.Sum(x=>x.Price*x.Count)
            },
        };
        
        orderCreateDto.OrderItem.ForEach(item =>
        {
            orderCreatedEvent.OrderItems.Add(new OrderItemMessage
            {
                Count = item.Count,
                ProductId = item.ProductId,
                
            });
        });
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMqSettingsConst.OrderSaga}"));
        await endpoint.Send<OrderCreatedRequestEvent>(orderCreatedEvent); 
        //Publish ile gönderirsek exchange'ye gider yani birinin subscribe olması gerekir subscribe olan birden fazla servis burdaki eventi alabilir,
        //Send ile gönderirsek kuyruk ismi belirtmemiz gerekir ve bir tane servis dinlerken kullanılır
        
        return Ok();
    }
}