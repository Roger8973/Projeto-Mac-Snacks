using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CartPurchaseController1 : Controller
    {
        private readonly ISnacksRepository _snackRepository;
        private readonly CartPurchase _cartPurchase;

        public CartPurchaseController1(ISnacksRepository snackRepository, CartPurchase cartPurchase)
        {
            _snackRepository = snackRepository;
            _cartPurchase = cartPurchase;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
