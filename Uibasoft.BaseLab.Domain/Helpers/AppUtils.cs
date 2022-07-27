using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uibasoft.BaseLab.Domain.Helpers
{
    public class AppUtils
    {
        //public static string GetMongoConnectionString(MongoDbSettings config)
        //{
        //    var usuario = config.UserName;
        //    var password = Uri.EscapeDataString(config.Password);
        //    var server = config.HostName;
        //    var port = config.Port;
        //    var connectString = $"mongodb://{usuario}:{password}@{server}:{port}";
        //    return connectString;
        //}

        public static string UrlComplete(string url)
        {
            var llave = url;
            if (llave.EndsWith("/"))
                llave = llave.Substring(0, llave.Length - 1);
            return llave;
        }
    }
}
