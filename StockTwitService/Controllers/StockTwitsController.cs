using System;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using RestSharp;
using StockTwitService.Models;

namespace StockTwitService.Controllers
{
    public class StockTwitsController : ApiController
    {
        const string StockTwitUrl = "https://api.stocktwits.com/";
        readonly RestClient _client;

        public StockTwitsController()
        {
            _client = new RestClient(StockTwitUrl);

        }

        [Route("api/searchNewsBySymbol"), HttpGet]
        public IHttpActionResult SearchNewsBySymbol(string symbol = null, string limit = "5")
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                var response = new StockTwitResponse
                {
                    response = new Response { status = 404 },
                    errors = new[] { new Error { message = "Please provide a Symbol to find." } }
                };
                return Ok(response);
            }

            try
            {
                var request = new RestRequest($"api/2/streams/symbol/{symbol}.json", Method.GET);
                request.AddQueryParameter("limit", limit);

                var tcs = new TaskCompletionSource<StockTwitResponse>();


                _client.ExecuteAsync(request, resp =>
                {

                    tcs.TrySetResult(JsonConvert.DeserializeObject<StockTwitResponse>(resp.Content));

                });

                return Ok(tcs.Task.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}