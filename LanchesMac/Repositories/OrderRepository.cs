using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CartPurchase _cartPurchase;

        public OrderRepository(AppDbContext appDbContext, CartPurchase cartPurchase)
        {
            _appDbContext = appDbContext;
            _cartPurchase = cartPurchase;
        }

        public void CreateOrder(Order order)
        {
            order.OrderDispatched = DateTime.Now;
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var cartPurchaseItems = _cartPurchase.CartPurchaseItems;

            foreach (var items in cartPurchaseItems)
            {
                var orderDetails = new OrderDetail
                {
                    Amount = items.Amount,
                    SnackId = items.Snack.SnackId,
                    OrderId = order.OrderId,
                    Price = items.Snack.Price
                };
                _appDbContext.OrderDetails.Add(orderDetails);
            }
            _appDbContext.SaveChanges();
        }
    }
}
