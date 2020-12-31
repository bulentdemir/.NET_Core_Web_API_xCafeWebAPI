using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xCafeWebAPI.Models
{
    public class ReservationProductJoin
    {
        public int ReservationProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }

        public ReservationProductJoin(int ReservationProductID, int Quantity, decimal TotalPrice, int ProductID, string ProductName, decimal ProductPrice, string ProductImage)
        {
            this.ReservationProductID = ReservationProductID;
            this.Quantity = Quantity;
            this.TotalPrice = TotalPrice;
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.ProductPrice = ProductPrice;
            this.ProductImage = ProductImage;
        }
    }
}
