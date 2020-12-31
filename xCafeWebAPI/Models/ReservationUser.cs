using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xCafeWebAPI.Models
{
    public class ReservationUser
    {
        public int ReservationID { get; set; }
        public DateTime ReservationTime { get; set; }
        public Boolean IsFinished{ get; set; }
        public Boolean IsCanceled{ get; set; }
        public decimal ReservationPrice { get; set; }
        public string UserID { get; set; }
        public string UserFullName { get; set; }
        public int TableID { get; set; }

        public ReservationUser(int ReservationID, DateTime ReservationTime, Boolean IsFinished, Boolean IsCanceled, decimal ReservationPrice, string UserID, string UserFullName, int TableID)
        {
            this.ReservationID = ReservationID;
            this.ReservationTime = ReservationTime;
            this.IsFinished = IsFinished;
            this.IsCanceled = IsCanceled;
            this.ReservationPrice = ReservationPrice;
            this.UserID = UserID;
            this.UserFullName = UserFullName;
            this.TableID = TableID;
        }
    }
}
