using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseWebApplication.Models
{
    public class Bid
    {
        [Required]
        public int ItemNumber { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        [MinLength(4)]
        public string CustomName { get; set; }
        [Required]
        [Range(10000000, 99999999, ErrorMessage = "Værdi skal være 8 sammenhængende cifre")]
        public int CustomPhone { get; set; }
    }
}
