using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xCafeWebAPI.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCanceled { get; set; }
        public decimal ReservationPrice { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        [ForeignKey("Table")]
        public int TableID { get; set; }

        //Foreign Keys
        public virtual ApplicationUser User { get; set; }
        public virtual Table Table { get; set; }

        public ICollection<ReservationProduct> ReservationProducts { get; set; }

    }
}
