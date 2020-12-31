using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xCafeWebAPI.Models
{
    public class Table
    {
        public int TableID { get; set; }
        public int TableCapacity { get; set; }
        public Boolean IsActive { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
