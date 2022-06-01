using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CartPurchaseController : Controller
    {
        private readonly ISnacksRepository _snackRepository;
        private readonly CartPurchase _cartPurchase;

        public CartPurchaseController(ISnacksRepository snackRepository, CartPurchase cartPurchase)
        {
            _snackRepository = snackRepository;
            _cartPurchase = cartPurchase;
        }

        public IActionResult Index()
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
        [Authorize]
        public IActionResult AddItemToCart(int snackId)
        {
            var selectSnack = _snackRepository.Snacks.FirstOrDefault(p => p.SnackId == snackId);

            if (selectSnack != null)
            {
                _cartPurchase.AddToCart(selectSnack);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItemShoppingCart(int snackId)
        {
            var selectSnack = _snackRepository.Snacks.FirstOrDefault(p => p.SnackId == snackId);

            if (selectSnack != null)
            {
                _cartPurchase.RemoveFromCart(selectSnack);
            }

            return RedirectToAction("Index");
        }
    }
}
