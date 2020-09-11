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
    public class ProsumerController
    {
        static string mApiUrl = "http://localhost:50955/api/Prosumer";

        static HttpClient mHttpClient = new HttpClient();

        //// GET: api/Prosumer
        public async Task<List<Prosumer>> GetAllProsumersAsync()
        {
            string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl));
            return JsonConvert.DeserializeObject<List<Prosumer>>(responseBody);
        }

        //// GET: api/Prosumer/5
        public async Task<Prosumer> GetProsumerAsync(string id)
        {
            string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl + "/" + id));
            return JsonConvert.DeserializeObject<Prosumer>(responseBody);
        }

        //// GET UNDER/OVERPRODUCING: api/Prosumer/5
        public async Task<List<Prosumer>> GetProsumerWithProduction(string producing)
        {
            if(producing == "Overproducing" || producing == "Underproducing")
            {
                string responseBody = await mHttpClient.GetStringAsync(new Uri(mApiUrl + "/" + producing));
                return JsonConvert.DeserializeObject<List<Prosumer>>(responseBody);
            }
            else
            {
                return null;
            }
        }

        //// PUT: api/People/5
        public async Task<HttpResponseMessage> Put(string id, Prosumer prosumer)
        {
            var content = JsonConvert.SerializeObject(prosumer);
            var buf = System.Text.Encoding.UTF8.GetBytes(content);
            var byteArrayContent = new ByteArrayContent(buf);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseBody = await mHttpClient.PutAsync(new Uri(mApiUrl + "/" + id), byteArrayContent);
            return responseBody;
        }

        // POST: api/Prosumer
        public async Task<HttpResponseMessage> Post(Prosumer prosumer)
        {
            var content = JsonConvert.SerializeObject(prosumer);
            var buf = System.Text.Encoding.UTF8.GetBytes(content);
            var byteArrayContent = new ByteArrayContent(buf);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseBody = await mHttpClient.PostAsync(new Uri(mApiUrl), byteArrayContent);
            return responseBody;
        }

        //// DELETE: api/People/5
        public async Task<HttpResponseMessage> Delete(string id)
        {
            var responseBody = await mHttpClient.DeleteAsync(new Uri(mApiUrl + "/" + id));
            return responseBody;
        }
        //[HttpDelete("{name}")]
        //public async Task<IActionResult> DeleteProsumer([FromRoute] string name)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Prosumer prosumer = repo.GetProsumer(name);

        //    if (prosumer == null)
        //    {
        //        return NotFound();
        //    }

        //    await repo.Remove(prosumer);

        //    return Ok(prosumer);
        //}
    }
}