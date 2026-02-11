using EcommerceDev.Application.Common;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;
using EcommerceDev.Core.Services;
using EcommerceDev.Infrastructure.BackgroundJobs;
using EcommerceDev.Infrastructure.Events;
using EcommerceDev.Infrastructure.Geolocation;
using EcommerceDev.Infrastructure.Messaging;
using Hangfire;
using Microsoft.Extensions.Options;

namespace EcommerceDev.Application.Commands.Orders.CreateOrder;

public class CreateOrderCommandHandler : IHandler<CreateOrderCommand, ResultViewModel<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IGeolocationService _geolocationService;
    private readonly IOrderDomainService _orderDomainService;
    private readonly ICustomerRepository _customerRepository;
    private readonly GeolocationSettings _geolocationSettings;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IEventPublisher eventPublisher,
        IGeolocationService geolocationService,
        IOrderDomainService orderDomainService,
        ICustomerRepository customerRepository,
        IOptions<GeolocationSettings> geolocationSettingsOptions,
        IBackgroundJobClient backgroundJobClient)
    {
        _orderRepository = orderRepository;
        _eventPublisher = eventPublisher;
        _geolocationService = geolocationService;
        _orderDomainService = orderDomainService;
        _customerRepository = customerRepository;
        _geolocationSettings = geolocationSettingsOptions.Value;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<ResultViewModel<Guid>> HandleAsync(CreateOrderCommand request)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.IdCustomer);
        if (customer == null) return ResultViewModel<Guid>.Error("Customer not found.");

        var address = await _customerRepository.GetAddressAsync(request.DeliveryAddressId);
        if (address == null) return ResultViewModel<Guid>.Error("Address not found.");

        var totalShippingCost = await CalculateShipping(request, address.GetFullAddress());
        if (totalShippingCost == -1) return ResultViewModel<Guid>.Error("Error when calculating shipping cost.");

        var order = new Order(
            request.IdCustomer,
            request.DeliveryAddressId,
            totalShippingCost,
            request.Items.Select(i => new OrderItem(i.IdProduct, i.Quantity)).ToList()
        );

        await _orderDomainService.UpdateProductPrices(order);

        var totalProductPrice = await _orderDomainService.CalculateProductOrderTotal(order.Items);

        order.SetTotalProductsPrice(totalProductPrice);

        await _orderRepository.CreateOrderAsync(order);

        var @event = new OrderCreatedEvent(order.Id);

        await _eventPublisher.PublishAsync(@event);

        _backgroundJobClient.Enqueue<SendOrderConfirmationEmailJob>(job =>
            job.ExecuteAsync(order.Id, customer.Email));

        return ResultViewModel<Guid>.Success(order.Id);
    }

    private async Task<decimal> CalculateShipping(CreateOrderCommand request, string destination)
    {
        var distanceInKm = await _geolocationService.GetDistance(_geolocationSettings.Origin, destination);

        var items = request.Items.Select(i => new OrderItem(i.IdProduct, i.Quantity)).ToList();

        var totalShippingCost = _orderDomainService.CalculateShippingCost(distanceInKm, items);

        return totalShippingCost;
    }
}