using EcommerceDev.Application.Common;
using EcommerceDev.Core.Repositories;

namespace EcommerceDev.Application.Queries.Orders.GetOrderById;

public class GetOrderByIdQueryHandler : IHandler<GetOrderByIdQuery, ResultViewModel<GetOrderDetailsViewModel>>
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<ResultViewModel<GetOrderDetailsViewModel>> HandleAsync(GetOrderByIdQuery request)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        if (order is null) return ResultViewModel<GetOrderDetailsViewModel>.Error("Order not found!");
        return ResultViewModel<GetOrderDetailsViewModel>.Success(GetOrderDetailsViewModel.ToViewModel(order));
    }
}
