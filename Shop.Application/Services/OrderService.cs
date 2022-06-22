using Shop.Application.Interfaces;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Orders;
using Shop.Domain.Models.Wallet;
using Shop.Domain.ViewModels.Account;
using Shop.Domain.ViewModels.Admin.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public  class OrderService: IOrderService
    {
        #region constractor
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWalletRepository _walletRepository;
        public OrderService(IOrderRepository orderRepository , IProductRepository productRepository , IWalletRepository walletRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _walletRepository = walletRepository;
        }

        #endregion

        #region order
        public async  Task<long> AddOrder(long userId, long productId)
        {
            var product = await _productRepository.GetProductById(productId);

            var order = await _orderRepository.CheckUserOrder(userId);

            if (order == null)
            {
                // Add Order

                order = new Order
                {
                    UserId = userId,
                    IsDelete = false,
                    Ordersum = product.Price,
                    OrderState = OrderState.Processing,
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail()
                        {
                            ProductId = productId,
                            Price = product.Price,
                            Count = 1,
                        }
                    }
                };

                await _orderRepository.AddOrder(order);
                await _orderRepository.SaveChanges();

            }
            else
            {
                var detail = await _orderRepository.CheckOrderDetail(order.Id, product.Id);
                if (detail != null )
                {
                    detail.Count += 1;
                    _orderRepository.UpdateOrderDetail(detail);
                    //await _orderRepository.SaveChanges();
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        Count = 1,
                        ProductId = productId,
                        Price = product.Price,
                    };
                    await _orderRepository.AddOrderDetail(detail);
                    //await _orderRepository.SaveChanges();
                }

                await _orderRepository.SaveChanges();   

            }
            await UpdatePriceOrder(order.Id);
            return order.Id;
        }

        public async  Task ChangeIsFinallyToOrder(long orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            order.IsDelete = true;
            order.OrderState = OrderState.Requested;

            _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChanges();
        }

        public async  Task<bool> ChangeStateToSent(long orderId)
        {
            var currentorder = await _orderRepository.GetOrderById(orderId);

            if (currentorder != null)
            {
                currentorder.OrderState = OrderState.Sent;
                _orderRepository.UpdateOrder(currentorder);
                await _orderRepository.SaveChanges();

                return true;
            }

            return false;
        }

        public async  Task<FilterOrdersViewModel> FilterOrders(FilterOrdersViewModel filterOrders)
        {
            return await _orderRepository.FilterOrders(filterOrders);

        }

        public async  Task<FinallyOrderResult> FinallyOrder(FinallyOrderViewModel finallyOrder, long userId)
        {
            if (userId != finallyOrder.UserId)
            {
                return FinallyOrderResult.HasNotUser;
            }

            var order = await _orderRepository.GetOrderById(finallyOrder.OrderId , userId);

            if (order == null && order.IsFinaly==true)
            {
                return FinallyOrderResult.NotFound;
            }

            if (await _walletRepository.GetUserWalletAmount(userId) >= order.Ordersum)
            {
                order.IsFinaly = true;
                order.OrderState = OrderState.Requested;

                var wallet = new UserWallet
                {
                    Amount = order.Ordersum,
                    Description = $"فاکتور شماره {order.Id}",
                    IsPay = true,
                    WalletType = WalletType.Bardasht,
                    UserId = userId,
                };
                await _walletRepository .CreateWallet(wallet);
                _orderRepository.UpdateOrder(order);
                await _orderRepository.SaveChanges();

                return FinallyOrderResult.Suceess;
            }

            return FinallyOrderResult.Error;
        }

        public async  Task<Order> GetOrderById(long orderId)
        {
            return await _orderRepository.GetOrderById(orderId);    
        }

        public async  Task<Order> GetOrderDetail(long orderId)
        {
            return await _orderRepository.GetOrderDetail(orderId);
        }

        public async  Task<ResultOrderStateViewModel> GetResultOrder()
        {
            return await _orderRepository.GetResultOrder();
        }

        public async  Task<Order> GetUserBasKet(long orderId, long userId)
        {
            return await _orderRepository.GetUserBasKet(orderId, userId);   
        }

        public async  Task<Order> GetUserBasKet(long userId)
        {
            return await _orderRepository.GetUserBasKet(userId);
        }

        public async  Task<bool> RemoveOrderDetailFromOrder(long orderDetailId)
        {
           var orderDetail = await _orderRepository.GetOrderDetailById(orderDetailId);
            var order = await _orderRepository.GetOrderById(orderDetail.OrderId);

            if (orderDetail != null)
            {
                orderDetail.IsDelete = true;
                _orderRepository.UpdateOrderDetail(orderDetail);    
                order.Ordersum = (await _orderRepository.OrderSum(order.Id) - orderDetail.Price);
                _orderRepository.UpdateOrder(order);
                await _orderRepository.SaveChanges();
                return true;
            }

            return false;
        }

        public async  Task UpdatePriceOrder(long orderId)
        {
             var order =await _orderRepository.GetOrderById(orderId);
            order.Ordersum = await _orderRepository.OrderSum(orderId);

             _orderRepository.UpdateOrder(order);
            await _orderRepository.SaveChanges();
        }
        #endregion

    }
}
