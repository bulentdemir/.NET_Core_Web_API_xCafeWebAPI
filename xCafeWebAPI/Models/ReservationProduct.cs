using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xCafeWebAPI.Models
{
    public class ReservationProduct
    {
        public int ReservationProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        //Foreign Keys
        public virtual Reservation Reservation { get; set; }
        public virtual Product Product { get; set; }
    }
}
