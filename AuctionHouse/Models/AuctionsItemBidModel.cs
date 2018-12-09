using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouse.Models
{
    public class AuctionsItemBidModel
    {
        public int ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public int RatingPrice { get; set; }
        public int BidPrice { get; set; }
        public string BidCustomName { get; set; }
        public int? BidCustomPhone { get; set; }
        public DateTime? BidTimestamp { get; set; }
    }
}
