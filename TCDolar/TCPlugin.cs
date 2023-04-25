using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TCDolar.Model;
using Wox.Plugin;

namespace TCDolar
{
    public class TCPlugin : IPlugin
    {
        TC tc = new TC();
        API api = new API();

        public void Init(PluginInitContext context) { }
        public List<Result> Query(Query query)
        {
            if (tc != null)
            {
                GetTC();
                double amount = double.Parse(query.Search);
                double cr = tc.venta * amount;
                double usd = amount / tc.venta;

                return new List<Result>
                {
                    new Result
                    {
                        Title = "USD to CR",
                        SubTitle = $"¢{cr}",
                        IcoPath = "cr.png",
                        Action = _ =>
                        {
                            return true;
                        }
                    },

                    new Result
                    {
                        Title = "CR to USD",
                        SubTitle = $"${usd}",
                        IcoPath = "usd.png",
                        Action = _ =>
                        {
                            return true;
                        }
                    }
                };
            }
            else
            {
                return new List<Result>()
                {
                    new Result
                    {
                        Title = "Error",
                        SubTitle = "Error al cargar datos.",
                        Action = _ =>
                        {
                            return true;
                        }
                    }
                };
            }
        }

        public async void GetTC()
        {
            string url = api.URL();

            HttpClient client = api.APIInit();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                tc = JsonConvert.DeserializeObject<TC>(result);
            }
        }
    }
}