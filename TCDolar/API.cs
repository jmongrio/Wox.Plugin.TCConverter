using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TCDolar.Model;

namespace TCDolar
{
    public class API
    {
        public string URL()
        {
            return $"https://tipodecambio.paginasweb.cr/api";
        }

        public HttpClient APIInit()
        {
            string url = URL();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            return client;
        }

        //public async Task<TC> GetTC() 
        //{
        //    string url = URL();

        //    HttpClient client = APIInit();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = response.Content.ReadAsStringAsync().Result;
        //        TC data = JsonConvert.DeserializeObject<TC>(result);
        //        return data;
        //    }
        //    return null;
        //}
    }
}