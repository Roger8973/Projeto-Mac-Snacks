
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models
{
    public class CartPurchaseItem
    {
        public int CartPurchaseItemId { get; set; }
        public Snack Snack { get; set; }
        public int Amount { get; set; }

        [StringLength(200)]
        public string CartPurchaseId { get; set; }

    }
}
