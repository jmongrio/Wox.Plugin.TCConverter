using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TCDolar.Model;
using webScrapping;
using Wox.Plugin;

namespace TCDolar
{
    public class TCPlugin : IPlugin
    {
        TC tc = new TC();

        public void Init(PluginInitContext context) { }
        public List<Result> Query(Query query)
        {
            if (tc != null && tc.venta != 0)
            {
                tc.venta = GetTC();
                double amount = double.Parse(query.Search);
                double cr = tc.venta * amount;
                double usd = amount / tc.venta;

                CultureInfo usdCurrency = new CultureInfo("en-us");
                CultureInfo crCurrency = new CultureInfo("es-cr");

                return new List<Result>
                {
                    new Result
                    {
                        Title = "USD to CR",
                        SubTitle = $"{cr.ToString("C", crCurrency)}",
                        IcoPath = "cr.png",
                        Action = _ =>
                        {
                            return true;
                        }
                    },

                    new Result
                    {
                        Title = "CR to USD",
                        SubTitle = $"{usd.ToString("C", usdCurrency)}",
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

        public double GetTC()
        {
            string url = $"https://www.bccr.fi.cr/SitePages/Inicio.aspx";

            var response = webScrap.CallUrl(url).Result;
            string tcDolarString = webScrap.ParseHtml(response);

            double tcDolar = double.Parse(tcDolarString);

            return tcDolar;
        }
    }
}