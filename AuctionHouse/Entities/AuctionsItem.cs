using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouse.Entities
{
    public class AuctionsItem
    {
        [Key]
        public int ItemNumber { get; set; } // primary key
        public string ItemDescription { get; set; }
        public int RatingPrice { get; set; }
        public int BidId { get; set; }
        [ForeignKey("BidId")]
        public virtual Bid Bid { get; set; }
    }
}
