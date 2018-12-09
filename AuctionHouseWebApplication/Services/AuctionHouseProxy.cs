﻿using AuctionHouseWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouseWebApplication.Services
{
    public class AuctionHouseProxy
    {
        const string baseUrl = "http://localhost:51542/api/auction/";

        public async Task<IEnumerable<AuctionItemBidModel>> GetAuctionItemBidsAsync()
        {
            var url = $"{baseUrl}";
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<AuctionItemBidModel>>(json);
        }
        public async Task<AuctionItemBidModel> GetAuctionItemBidByIdAsync(int id)
        {
            var url = $"{baseUrl}{id}";
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<AuctionItemBidModel>(json);
        }

        public async Task<HttpResponseMessage> PostAuctionBidAsync(int id, Bid bid)
        {
            var cancellationToken = new CancellationToken();
            var url = $"{baseUrl}{id}/bid";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var json = JsonConvert.SerializeObject(bid);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    {
                        return response;
                    }
                }
            }
        }

        internal async Task<IEnumerable<Bidder>> GetBiddersAsync()
        {
            var url = $"{baseUrl}bidders";
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Bidder>>(json);
        }

        internal async Task<IEnumerable<UserBid>> GetBidsByPhoneAsync(int phone)
        {
            var url = $"{baseUrl}bidders/{phone}";
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<UserBid>>(json);
        }
    }
}
