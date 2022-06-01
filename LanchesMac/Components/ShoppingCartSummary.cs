using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly CartPurchase _cartPurchase;

        public ShoppingCartSummary(CartPurchase cartPurchase)
        {
            _cartPurchase = cartPurchase;
        }

        public IViewComponentResult Invoke()
        {
            var items = _cartPurchase.GetCartPurchaseItems();          
            _cartPurchase.CartPurchaseItems = items;

            var cartPurchaseVM = new CartPurchaseViewModel
            {
                CartPurchase = _cartPurchase,
                CartPurchaseTotal = _cartPurchase.TotalShoppingCart()
            };

            return View(cartPurchaseVM);
        }
    }
}
