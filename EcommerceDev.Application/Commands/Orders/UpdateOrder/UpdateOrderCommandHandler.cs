using EcommerceDev.Application.Common;
using EcommerceDev.Core.Entities;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Commands.Orders.UpdateOrder;

public class UpdateOrderCommandHandler : IHandler<UpdateOrderCommand, ResultViewModel>
{
    private readonly IOrderRepository _orderRepository;
    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ResultViewModel> HandleAsync(UpdateOrderCommand request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.IdOrder);
        if (order is null)
            return ResultViewModel.Error("Order not found!");

        order.ConfirmationDate = request.ConfirmationDate;
        order.ShippingDate = request.ShippingDate;
        order.Status = request.Status;
        order.ShippingPrice = request.ShippingPrice;
        order.TotalProductsPrice = request.TotalProductsPrice;

        await _orderRepository.UpdateOrderAsync(order);

        return ResultViewModel.Success();
    }
}
