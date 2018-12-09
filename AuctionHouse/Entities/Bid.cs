using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouse.Entities
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemNumber { get; set; } // FK til AuctionsItem
        [Required]
        public int Price { get; set; }
        [MinLength(5)]
        public string CustomName { get; set; }
        public int CustomPhone { get; set; }
        [ConcurrencyCheck]
        public DateTime Timestamp { get; set; }
        [ForeignKey("ItemNumber")]
        public virtual AuctionsItem AuctionsItem { get; set; }
    }
}
