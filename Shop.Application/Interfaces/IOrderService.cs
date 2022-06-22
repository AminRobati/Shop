using Shop.Domain.Models.Orders;
using Shop.Domain.ViewModels.Account;
using Shop.Domain.ViewModels.Admin.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces
{
    public  interface IOrderService
    {
        Task<long> AddOrder(long userId, long productId);
        Task UpdatePriceOrder(long orderId);
        Task<Order> GetUserBasKet(long orderId, long userId);
        Task<Order> GetUserBasKet(long userId);
        Task<FinallyOrderResult> FinallyOrder(FinallyOrderViewModel finallyOrder , long userId);    
        Task<bool> RemoveOrderDetailFromOrder(long orderDetailId);
        Task<Order> GetOrderById(long orderId);
        Task ChangeIsFinallyToOrder(long orderId);
        Task<ResultOrderStateViewModel> GetResultOrder();
        Task<FilterOrdersViewModel> FilterOrders(FilterOrdersViewModel filterOrders);

        Task<bool> ChangeStateToSent(long orderId);
        Task<Order> GetOrderDetail(long orderId);
    }
}
