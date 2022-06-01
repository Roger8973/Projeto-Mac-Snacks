using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CartPurchase _cartPurchase;

        public OrderController(IOrderRepository orderRepository, CartPurchase cartPurchase)
        {
            _orderRepository = orderRepository;
            _cartPurchase = cartPurchase;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            int totalItemsOrdered = 0;
            decimal totalAskingPrice = 0.0m;

            //obtem os itens do carrinho de compra do cliente
            List<CartPurchaseItem> cartPurchaseItems = _cartPurchase.GetCartPurchaseItems();
            _cartPurchase.CartPurchaseItems = cartPurchaseItems;

            //verifica se existe itens de pedido
            if (_cartPurchase.CartPurchaseItems.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");
            }

            //calcula o total de itens do pedido
            foreach (var items in cartPurchaseItems)
            {
                totalItemsOrdered += items.Amount;
                totalAskingPrice += (items.Snack.Price * items.Amount);
            }
            order.TotalOrder = totalAskingPrice;
            order.TotalItemsOrdered = totalItemsOrdered;

            //valida os dados do pedido
            //cria o pedido e os detalhes
            //define mensagens ao cliente
            //limpa o carrinho
            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                ViewBag.CheckoutMessage = "Obrigado pelo seu pedido :)";
                ViewBag.TotalOrder = _cartPurchase.TotalShoppingCart();

                _cartPurchase.CleanCart();

                return View("~/Views/Order/FullCheckout.cshtml", order);
            }

            return View(order);
        }
    }
}
