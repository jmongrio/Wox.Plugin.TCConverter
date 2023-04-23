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
            if (tc == null)
            {
                return new List<Result>()
                {
                    new Result
                    {
                        Title = "Error",
                        SubTitle = "Error al cargar datos",
                        Action = _ =>
                        {
                            return true;
                        }
                    }
                };
            }
            else
            {
                GetTC();
                double amount = double.Parse(query.Search);
                double compra = tc.compra * amount;
                double venta = tc.venta * amount;

                return new List<Result>
                {
                    new Result
                    {
                        Title = "Compra",
                        SubTitle = $"{query.Search} * {tc.compra} = {compra}",
                        IcoPath = "compra.png",
                        Action = _ =>
                        {
                            return true;
                        }
                    },

                    new Result
                    {
                        Title = "Venta",
                        SubTitle = $"{query.Search} * {tc.venta} = {venta}",
                        IcoPath = "venta.png",
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