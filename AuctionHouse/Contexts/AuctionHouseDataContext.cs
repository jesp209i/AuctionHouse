using AuctionHouse.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouse.Contexts
{
    public class AuctionHouseDataContext : DbContext
    {
        public AuctionHouseDataContext(DbContextOptions<AuctionHouseDataContext> options) : base(options)
        {
            Database.EnsureCreated();
            if (AuctionsItems.Count() == 0)
            {
                var bid = new Bid { ItemNumber = 123456, Price = 2100, CustomName = "Bjørk Boye Busch", CustomPhone = 63129162, Timestamp = DateTime.Now };
                //Bids.Add(bid);
                var auction = new AuctionsItem { ItemNumber = 123456, ItemDescription = "Ph 5 Classic lampe Hvid mat", RatingPrice = 2100, Bid = bid };
                var auction2 = new AuctionsItem { ItemNumber = 456789, ItemDescription = "Dette er en anden PH lampe, ukendt mærke", RatingPrice = 100};
                AuctionsItems.Add(auction);
                AuctionsItems.Add(auction2);
                SaveChanges();
            }
            
        }

        public DbSet<AuctionsItem> AuctionsItems { get; set; }
        public DbSet<Bid> Bids { get; set; }

    }
}
