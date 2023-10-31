using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStore.Order.Application.Orders.DTOs
{
    public class CouponDTO
    {
        public CouponDTO(string couponCode, decimal? discountAmount)
        {
            CouponCode = couponCode;
            DiscountAmount = discountAmount;
        }

        public string CouponCode { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}
