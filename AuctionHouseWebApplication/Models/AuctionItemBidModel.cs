using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseWebApplication.Models
{
    public class AuctionItemBidModel
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
