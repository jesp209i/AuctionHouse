using AuctionHouseWebApplication.Models;
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
        // Using wonderfull generics <3
        public async Task<T> GetEntityAsync<T>(string url) where T : class
        {
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<HttpResponseMessage> PostAuctionBidAsync(int id, Bid bid, string baseUrl)
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
    }
}
