using System.Collections.Generic;
using System.Globalization;
using TCDolar.Model;
using Wox.Plugin;

namespace TCDolar
{
    public class TCPlugin : IPlugin
    {
        public void Init(PluginInitContext context) { }
        public List<Result> Query(Query query)
        {
            ExchangeRate exchangeRate = new ExchangeRate();

            if (exchangeRate != null && exchangeRate.Venta != 0)
            {
                exchangeRate.Venta = GetTC();
                double amount = double.Parse(query.Search);
                double cr = exchangeRate.Venta * amount;
                double usd = amount / exchangeRate.Venta;

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
    }
}