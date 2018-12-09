using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Contexts;
using AuctionHouse.Entities;
using AuctionHouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Controllers
{
    [Route("api/auction")]
    [Produces("application/json")]
    [ApiController]
    public class AuctionHouseController : ControllerBase
    {
        private readonly AuctionHouseDataContext auctionHouseDataContext;

        public AuctionHouseController(AuctionHouseDataContext ahdc)
        {
            auctionHouseDataContext = ahdc;
        }
        [HttpGet]
        public IActionResult GetAllAuctionItems()
        {
            var auctions = auctionHouseDataContext.AuctionsItems.ToList();
            //var auctions = auctionHouseDataContext.AuctionsItems.Include(x => x.Bid).ToList();
            var auctionModels = new List<AuctionsItemBidModel>();
            foreach (var auction in auctions) {
                auctionModels.Add(ConvertAuctionItemToModel(auction));
            }
            return Ok(auctionModels);
        }
        private AuctionsItemBidModel ConvertAuctionItemToModel(AuctionsItem auction)
        {
            var auctionmodel = new AuctionsItemBidModel
            {
                ItemNumber = auction.ItemNumber,
                ItemDescription = auction.ItemDescription,
                RatingPrice = auction.RatingPrice,
                BidPrice = 0,
                BidCustomName = "",
                BidCustomPhone = null,
                BidTimestamp = null
            };
            auction.Bid = auctionHouseDataContext.Bids.FirstOrDefault(x => x.Id == auction.BidId);
            if (auction.Bid != null) {
                auctionmodel.BidPrice = auction.Bid.Price;
                auctionmodel.BidCustomName = auction.Bid.CustomName;
                auctionmodel.BidCustomPhone = auction.Bid.CustomPhone;
                auctionmodel.BidTimestamp = auction.Bid.Timestamp;
            }
            return auctionmodel;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAuctionItem([FromRoute]int id)
        {
            var auction = auctionHouseDataContext.AuctionsItems.FirstOrDefault(x => x.ItemNumber == id);
            if (auction != null) { return Ok(ConvertAuctionItemToModel(auction)); }
            return NotFound();
        }
        [HttpPost("{itemNumber:int}/bid")]
        public IActionResult ProvideBid([FromRoute]int itemNumber, [FromBody] Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (itemNumber != bid.ItemNumber) {
                return BadRequest(new { Error="itemNumber and bid itemnumber is not the same"});
            }
            var auction = auctionHouseDataContext.AuctionsItems.FirstOrDefault(x => x.ItemNumber == itemNumber);
            auction.Bid = auctionHouseDataContext.Bids.FirstOrDefault(x => x.Id == auction.BidId);
            if (auction != null)   // auction exists
            {
                if (auction.Bid == null || auction.Bid.Price < bid.Price)
                {
                    bid.Timestamp = DateTime.Now;
                    auction.Bid = null;
                    auction.Bid = bid;
                    auctionHouseDataContext.AuctionsItems.Update(auction);
                    auctionHouseDataContext.SaveChanges();
                    return Ok();
                }
                if (auction.Bid.Price >= bid.Price){
                    return BadRequest(new { Error= "Bid too low, current bid is " +auction.Bid.Price });
                }
                
            }
            return NotFound(new { Error = "Item not found" });
        }

        [HttpGet("bidders")]
        public IActionResult GetBidders()
        {
            var bidders = auctionHouseDataContext.Bids.Select(x => new { x.CustomName, x.CustomPhone }).Distinct();
            return Ok(bidders);
        }
        [HttpGet("bidders/{phone:int}")]
        public IActionResult GetBiddersBid([FromRoute]int phone)
        {
            var bids = auctionHouseDataContext.Bids.
                Where(x => x.CustomPhone == phone).
                Include(x=>x.AuctionsItem).
                Select(x=>new {x.ItemNumber, x.CustomName, x.CustomPhone, x.Price, WinningBid = x.AuctionsItem.BidId == x.Id , x.Timestamp });

            if (bids.Count() > 0)
            {
                return Ok(bids);
            }
            return NotFound();
        }
    }
}