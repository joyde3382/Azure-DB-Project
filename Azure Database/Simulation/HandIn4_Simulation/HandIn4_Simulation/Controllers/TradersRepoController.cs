using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using HandIn4_Simulation.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HandIn4_Simulation.Controllers
{
    public class TradersRepoController
    {
        static string mApiUrl = "http://localhost:50955/api/TradersRepo";

        static HttpClient mHttpClient = new HttpClient();

        // GET: api/TradersRepo
        public async Task<List<Trader>> GetAllTradersAsync()
        {
            string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl));
            return JsonConvert.DeserializeObject<List<Trader>>(responseBody);
        }

        //// GET: api/TradersRepo/5
        public async Task<Trader> GetTraderAsync(int id)
        {
            string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl + "/" + id.ToString()));
            return JsonConvert.DeserializeObject<Trader>(responseBody);
        }

        //// PUT: api/TradersRepo/5


        //// POST: api/TradersRepo
        public async Task<HttpResponseMessage> Post(Trader trader)
        {
            var content = JsonConvert.SerializeObject(trader);
            var buf = System.Text.Encoding.UTF8.GetBytes(content);
            var byteArrayContent = new ByteArrayContent(buf);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseBody = await mHttpClient.PostAsync(new Uri(mApiUrl), byteArrayContent);
            return responseBody;
        }

        //// DELETE: api/TradersRepo/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var responseBody = await mHttpClient.DeleteAsync(new Uri(mApiUrl + "/" + id));
            return responseBody;
        }

        //private bool TraderExists(int id)
        //{
        //    var Trader = _context.GetById(id);

        //    if (Trader == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}