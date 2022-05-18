using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CartPurchase
    {
        private readonly AppDbContext _context;

        public CartPurchase(AppDbContext context)
        {
            _context = context;
        }

        public string CartPurchaseId { get; set; }
        public List<CartPurchaseItem> CartPurchaseItems { get; set; }

        public static CartPurchase GetCartPurchase(IServiceProvider services)
        {
            //define uma sessao
            ISession session = services.GetRequiredService<HttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera um Id do carrinho
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            //atribui o id do carinho na sessão
            session.SetString("CartId", cartId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CartPurchase(context)
            {
                CartPurchaseId = cartId
            };
        }

        public void AddToCart(Snack snack)
        {
            var cartPurchaseItem = _context.CartPurchaseItems.SingleOrDefault(
                s => s.Snack.SnackId == snack.SnackId &&
                s.CartPurchaseId == CartPurchaseId);

            if (cartPurchaseItem == null)
            {
                cartPurchaseItem = new CartPurchaseItem
                {
                    CartPurchaseId = CartPurchaseId,
                    Snack = snack,
                    Amount = 1
                };
                _context.CartPurchaseItems.Add(cartPurchaseItem);
            }
            else
            {
                cartPurchaseItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveFromCart(Snack snack)
        {
            var cartPurchaseItem = _context.CartPurchaseItems.SingleOrDefault(
               s => s.Snack.SnackId == snack.SnackId &&
               s.CartPurchaseId == CartPurchaseId);

            if (cartPurchaseItem != null)
            {
                if (cartPurchaseItem.Amount > 1)
                {
                    cartPurchaseItem.Amount--;
                }
            }
            else
            {
                _context.CartPurchaseItems.Remove(cartPurchaseItem);
            }
            _context.SaveChanges();
        }

        public List<CartPurchaseItem> GetCartPurchaseItens()
        {
            return CartPurchaseItems ??
                (CartPurchaseItems = _context.CartPurchaseItems.
                Where(c => c.CartPurchaseId == CartPurchaseId).
                Include(s => s.Snack).ToList());
        }

        public void CleanCart()
        {
            var cartItems = _context.CartPurchaseItems.Where(cart => cart.CartPurchaseId == CartPurchaseId);

            _context.CartPurchaseItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }

        public decimal GetCartTotalPurchase()
        {
            var total = _context.CartPurchaseItems.Where(c => c.CartPurchaseId == CartPurchaseId)
                .Select(c => c.Snack.Price * c.Amount).Sum();

            return total;
        }
    }
}
