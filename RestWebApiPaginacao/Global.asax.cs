using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace RestWebApiPaginacao
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //configurar o tipo da data da aplicação para transformar no padrão brasileiro utf-8 pt-br (isso vale para todos os models, controles e views)
            //O RETORNO SEMPRE SERÁ NO FORMATO BRASILEIRO MAS A INSERÇÃO ELE ACEITA TANTO O PADRÃO PT-BR QUANTO O US
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    Culture = CultureInfo.GetCultureInfo("pt-br"),
                    DateFormatString = "dd/MM/yyyy"
                }; 
        }
    }
}
