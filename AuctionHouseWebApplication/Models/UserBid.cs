using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseWebApplication.Models
{
    public class UserBid
    {
        public int ItemNumber { get; set; }
        public string CustomName { get; set; }
        public int CustomPhone { get; set; }
        public int Price { get; set; }
        public bool WinningBid { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
