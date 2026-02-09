using EcommerceDev.Core.Repositories;
using EcommerceDev.Infrastructure.Events;
using EcommerceDev.Infrastructure.Payment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EcommerceDev.Infrastructure.Messaging.Consumers;

public class OrderCreatedEventConsumer : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IChannel _channel;

    public OrderCreatedEventConsumer(IServiceProvider serviceProvider, RabbitMqSettings settings)
    {
        _settings = settings;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeRabbitMqAsync();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<OrderCreatedEvent>(message);
                string? customerPaymentId;

                Console.WriteLine($"[Consumer] Received OrderCreatedEvent with Id {@event.IdOrder}");

                var scope = _serviceProvider.CreateScope();
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                var order = await orderRepository.GetOrderByIdAsync(@event.IdOrder);
                if (order is null)
                {
                    Console.WriteLine($"[Consumer] Order with Id {@event.IdOrder} does not exist");
                    return;
                }

                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                var customer = await customerRepository.GetCustomerById(order.IdCustomer);
                if(customer is null)
                {
                    Console.WriteLine($"[Consumer] Customer does not exist");
                    return;
                }

                var customerPaymentModel = new PaymentCustomerModel
                {
                    Email = customer.Email,
                    FullName = customer.FullName,
                    PhoneNumber = customer.PhoneNumber
                };

                if (customer.IdExternalPayment != null)
                    customerPaymentId = customer.IdExternalPayment;
                else
                {
                    customerPaymentId = await paymentService.CreateCustomerAsync(customerPaymentModel);
                    customer.IdExternalPayment = customerPaymentId;
                    await customerRepository.UpdateCustomerAsync(customer);
                }

                var orderPaymentModel = new PaymentOrderModel
                {
                    IdExternalCustomer = customerPaymentId,
                    Items = order.Items.Select(i => new PaymentOrderItemModel
                    {
                        Name = i.Product.Title,
                        Price = i.Price,
                        Quantity = i.Quantity
                    }).ToList()
                };

                var paymentResult = await paymentService.CreateOrderAsync(orderPaymentModel);

                order.MarkAsPaymentPending();
                order.IdExternalOrder = paymentResult.Id;
                order.PaymentUrl = paymentResult.Url;

                await orderRepository.UpdateOrderAsync(order);

                Console.WriteLine($"[Consumer] Order with Id {@event.IdOrder} updated");

                await _channel.BasicAckAsync(eventArgs.DeliveryTag, false, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Consumer] Exception: {ex.Message}");

                await _channel.BasicNackAsync(eventArgs.DeliveryTag, false, true, stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: _settings.QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
    }

    private async Task InitializeRabbitMqAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
        };

        _connection = factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;

        // Declare Exchange
        await _channel.ExchangeDeclareAsync(
            exchange: _settings.ExchangeName,
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false
        );

        // Declare Queue
        await _channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        // Bind queue to exchange with routing key
        await _channel.QueueBindAsync(
            queue: _settings.QueueName,
            exchange: _settings.ExchangeName,
            routingKey: "ordercreated");

        Console.WriteLine($"[Consumer] RabbitMQ initialized - Exchange: {_settings.ExchangeName}, Queue: {_settings.QueueName}");
    }
}

