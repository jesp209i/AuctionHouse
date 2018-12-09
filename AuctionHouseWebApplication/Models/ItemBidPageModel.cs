using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseWebApplication.Models
{
    public class ItemBidPageModel
    {
        [Required]
        public int ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public int RatingPrice { get; set; }
        [Required]
        public int BidPrice { get; set; }
        [Required]
        [MinLength(4)]
        public string BidCustomName { get; set; }
        [Required]
        [Range(10000000, 99999999, ErrorMessage = "Værdi skal være 8 sammenhængende cifre")]
        public int BidCustomPhone { get; set; }
        public DateTime? BidTimestamp { get; set; }
    }
}
