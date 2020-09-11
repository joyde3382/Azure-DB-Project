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
    class SmartGridsController
    {
        static string mApiUrl = "http://localhost:50955/api/SmartGrids";

        static HttpClient mHttpClient = new HttpClient();

        // GET: api/SmartGrids
        public async Task<List<SmartGrid>> GetAllSmartGridsAsync()
        {
            string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl));
            return JsonConvert.DeserializeObject<List<SmartGrid>>(responseBody);
        }

        // GET: api/SmartGrids/5
        public async Task<SmartGrid> GetSmartGridAsync(int id)
        {
            string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl + "/" + id.ToString()));
            return JsonConvert.DeserializeObject<SmartGrid>(responseBody);
        }

        // POST: api/SmartGrids
        public async Task<HttpResponseMessage> Post(SmartGrid smartGrid)
        {
            var content = JsonConvert.SerializeObject(smartGrid);
            var buf = System.Text.Encoding.UTF8.GetBytes(content);
            var byteArrayContent = new ByteArrayContent(buf);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseBody = await mHttpClient.PostAsync(new Uri(mApiUrl), byteArrayContent);
            return responseBody;
        }

        // DELETE: api/SmartGrids
        public async Task<HttpResponseMessage> Delete(int id)
        {
            var responseBody = await mHttpClient.DeleteAsync(new Uri(mApiUrl + "/" + id.ToString()));
            return responseBody;
        }

    }
}
