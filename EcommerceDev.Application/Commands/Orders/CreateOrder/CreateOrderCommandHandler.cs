using EcommerceDev.Application.Common;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Events;
using EcommerceDev.Infrastructure.Messaging;

namespace EcommerceDev.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandHandler : IHandler<CreateOrderCommand, ResultViewModel<Guid>>
{
    private readonly IOrderRepository _repository;
    private readonly IEventPublisher _eventPublisher;

    public CreateOrderCommandHandler(IOrderRepository repository, IEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
    }

    public async Task<ResultViewModel<Guid>> HandleAsync(CreateOrderCommand request)
    {
        var order = new Order(
            request.IdCustomer,
            request.DeliveryAddressId,
            100,
            1_000,
            request.Items.Select(i => new OrderItem(i.IdProduct, i.Quantity, 5)).ToList()
        );

        await _repository.CreateOrderAsync(order);

        var @event = new OrderCreatedEvent(order.Id);
        await _eventPublisher.PublishAsync(@event);

        return ResultViewModel<Guid>.Success(order.Id);
    }
}