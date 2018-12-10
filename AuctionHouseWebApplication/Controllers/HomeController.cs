using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuctionHouseWebApplication.Models;
using AuctionHouseWebApplication.Services;
using System.Net.Http;

namespace AuctionHouseWebApplication.Controllers
{

    public class HomeController : Controller
    {
        private readonly AuctionHouseProxy auctionHouseProxy;
        const string baseUrl = "http://localhost:51542/api/auction/";

        public HomeController(AuctionHouseProxy ahp)
        {
            auctionHouseProxy = ahp;
        }
        public async Task<IActionResult> Index()
        {
            var auctionList = await auctionHouseProxy.GetEntityAsync<List<AuctionItemBidModel>>(baseUrl);
            return View(auctionList);
        }
        [HttpGet("auction/{id:int}/Bid")]
        public async Task<IActionResult> AuctionBid([FromRoute]int id)
        {
            var auction = await auctionHouseProxy.GetEntityAsync<AuctionItemBidModel>(baseUrl + id);
            if (auction.BidCustomPhone == null) auction.BidCustomPhone = 0;
            var auctionBidPage = new ItemBidPageModel
            {
                ItemNumber = auction.ItemNumber,
                ItemDescription = auction.ItemDescription,
                RatingPrice = auction.RatingPrice,
                BidPrice = auction.BidPrice,
                BidCustomName = auction.BidCustomName,
                BidCustomPhone = (int)auction.BidCustomPhone,
                BidTimestamp = auction.BidTimestamp

            };
                return View(auctionBidPage);
        }
        [HttpPost("auction/{id:int}/Bid")]
        public async Task<IActionResult> SendAuctionBid([FromRoute]int id, [FromForm] ItemBidPageModel sendbid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Bid bid = new Bid {
                ItemNumber = sendbid.ItemNumber,
                Price = sendbid.BidPrice,
                CustomName = sendbid.BidCustomName,
                CustomPhone = sendbid.BidCustomPhone
            };
            var auction = await auctionHouseProxy.GetEntityAsync<AuctionItemBidModel>(baseUrl + id);
            HttpResponseMessage response = null;
                response = await auctionHouseProxy.PostAuctionBidAsync(id, bid, baseUrl);
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AuctionBid",new { id = id});
            }
            return RedirectToAction("AuctionBid", new { id = id });
        }
        [Route("bidders")]
        public async Task<IActionResult> Bidders()
        {
            var bidders = await auctionHouseProxy.GetEntityAsync<List<Bidder>>(baseUrl+"bidders");
            return View(bidders);
        }

        [Route("bidder/{id:int}")]
        public async Task<IActionResult> BidderDetails([FromRoute] int id)
        {
            var url = baseUrl + "bidders/" + id;
            var bids = await auctionHouseProxy.GetEntityAsync<List<UserBid>>(url);
            return View(bids);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
