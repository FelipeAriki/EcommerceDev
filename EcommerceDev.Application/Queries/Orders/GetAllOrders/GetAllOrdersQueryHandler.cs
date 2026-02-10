using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Queries.Orders.GetAllOrders;

public class GetAllOrdersQueryHandler : IHandler<GetAllOrdersQuery, ResultViewModel<IEnumerable<GetAllOrdersItemViewModel>>>
{
    private readonly IOrderRepository _orderRepository;
    public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ResultViewModel<IEnumerable<GetAllOrdersItemViewModel>>> HandleAsync(GetAllOrdersQuery request)
    {
        var orders = await _orderRepository.GetOrdersAsync();
        if (orders is null) return ResultViewModel<IEnumerable<GetAllOrdersItemViewModel>>.Error("Produto não encontrado!");
        return ResultViewModel<IEnumerable<GetAllOrdersItemViewModel>>.Success(orders.Select(order => new GetAllOrdersItemViewModel()
        {
            IdOrder = order.Id,
            IdCustomer = order.IdCustomer,
            ConfirmationDate = order.ConfirmationDate,
            ShippingDate = order.ShippingDate,
            Status = order.Status,
            DeliveryAddressId = order.DeliveryAddressId,
            ShippingPrice = order.ShippingPrice,
            TotalProductsPrice = order.TotalProductsPrice,
            IdExternalOrder = order.IdExternalOrder,
            PaymentUrl = order.PaymentUrl,
        }).ToList());
    }
}
