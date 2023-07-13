using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Cart.Data.DTOs
{
    public class CartDTO
    {
        public Guid UserId { get; set; }
        public IEnumerable<CartItemDTO> Items { get; set; }
    }

    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
